// これは メイン DLL ファイルです。

#include "stdafx.h"
#include <vector>
#include <algorithm>
#include <functional>
#include <random>
#include <chrono> 
#include "nlopt.hpp"
#include <msclr/gcroot.h>
#include "FittingSolverCore.h"

using namespace std;
using namespace msclr;
using namespace System;
using namespace System::Collections::Generic;
using namespace TTCDataUtils;
using namespace System::IO;
using namespace System::Text;
using namespace System::Threading;
namespace MagicFormulaFittingSolver
{

	struct NLOptFittingSolverCore
	{
	public :
		int Maxeval = 50000;
		int ConstrainsDependOnDataMaxeval = 5000;
		double Xtol = 1e-3;
		nlopt::algorithm Algorithm = nlopt::LN_COBYLA;
	public :
		int Run(TTCDataUtils::ApproximatingCurve^ curve, List<TireData^>^ dataList, CancellationTokenSource^ cancel, IProgress<TTCDataUtils::ProgressNotification^>^ prg)
		{

			int numParameters = 0;
			for (int i = 0; i < curve->Parameters->Count; ++i)
			{
				if (curve->FittingParameters[i]) ++numParameters;
			}
			int numData = dataList->Count;
			vector<double> p(numParameters);
			GetListDouble(p, curve->Parameters, curve->FittingParameters);

			//最小化ソルバ
			nlopt::opt opt(Algorithm, numParameters);
			//ソルバーに関数登録
			auto objf = new ObjectiveFunction(curve, dataList, cancel, prg, numParameters);
			opt.set_min_objective(ObjectiveFunction::Function, objf);

			//最小最大値制約
			std::vector<double> lb(numParameters, -HUGE_VAL);
			opt.set_lower_bounds(lb);
			std::vector<double> ub(numParameters, HUGE_VAL);
			opt.set_upper_bounds(ub);

			//その他の制約

			//データに寄らない制約
			int numConstrainsPureCount = curve->ConstraintsPure()->Count;
			vector<ConstrainsFunction> constrainsPure(numConstrainsPureCount);
			for (int i = 0; i < numConstrainsPureCount; ++i)
			{
				constrainsPure[i].curve = curve;
				constrainsPure[i].dataList = dataList;
				constrainsPure[i].numConstrains = i;
				constrainsPure[i].numData = -1;
				constrainsPure[i].cancel = cancel;
				opt.add_inequality_constraint(&ConstrainsFunction::Constrains, &constrainsPure[i], Xtol);
			}


			//データに依存する制約
			int maxEval = min(ConstrainsDependOnDataMaxeval, numData);
			int numConstrainsDependOnData = curve->ConstraintsDependOnData()->Count*maxEval;

			vector<int> index(numData);
			for (int i = 0; i < index.size(); ++i)
			{
				index[i] = i;
			}
			unsigned seed = std::chrono::system_clock::now().time_since_epoch().count();
			std::shuffle(index.begin(), index.end(), std::default_random_engine(seed));

			vector<ConstrainsFunction> constrainsDependOnData(numConstrainsDependOnData);
			for (int i = 0; i < numConstrainsDependOnData; ++i)
			{
				constrainsDependOnData[i].curve = curve;
				constrainsDependOnData[i].dataList = dataList;
				constrainsDependOnData[i].numConstrains = i / maxEval;
				constrainsDependOnData[i].numData = index[i % maxEval];
				constrainsDependOnData[i].cancel = cancel;
				opt.add_inequality_constraint(&ConstrainsFunction::Constrains, &constrainsDependOnData[i], Xtol);
			}

			//収束設定
			opt.set_xtol_rel(Xtol);
			opt.set_maxeval(Maxeval);
			double minf;
	 		nlopt::result result;
			try
			{
				result = opt.optimize(p, minf);
			}
			catch (...)
			{
				if (objf->canceled)
				{
					throw gcnew OperationCanceledException("User Cancel");
				}
				else
				{
					throw gcnew Exception("Failure");
				}
				
			}

			return result;

		}

		static void GetListDouble(vector<double>& p, List<double>^ params, List<bool>^ enable)
		{
			int j = 0;
			for (int i = 0; i < params->Count; ++i)
			{
				if (enable[i]) p[j++] = params[i];
			}
		}
		static void SetListDouble(List<double>^ params, const vector<double>& p, List<bool>^ enable)
		{
			int j = 0;
			for (int i = 0; i < params->Count; ++i)
			{
				if(enable[i]) params[i] = p[j++];
			}
		}

		struct ObjectiveFunction
		{
			ObjectiveFunction(msclr::gcroot<ApproximatingCurve^> c, msclr::gcroot<List<TireData^>^> d, msclr::gcroot<CancellationTokenSource^> can, msclr::gcroot<IProgress<ProgressNotification^>^> prg, int numParams_)
				:curve(c), dataList(d),cancel(can), progress(prg),count(0),numParams(numParams_)
				{
					canceled = false;
				}
			int numParams;
			int count;
			bool canceled;
			msclr::gcroot<ApproximatingCurve^> curve;
			msclr::gcroot<List<TireData^>^> dataList;
			msclr::gcroot<System::Threading::CancellationTokenSource^> cancel;
			msclr::gcroot<IProgress<ProgressNotification^>^> progress;
			static double Function(const std::vector<double> &x, std::vector<double> &grad, void *data)
			{
				auto of = static_cast<ObjectiveFunction*>(data);
				++(of->count);
				int numParams = of->numParams;
				int numData = of->dataList->Count;
				SetListDouble(of->curve->Parameters, x, of->curve->FittingParameters);
				double result = 0;

				for (int i = 0; i < numData; ++i)
				{
					List<TireData^>^ dataListH = of->dataList;
					auto err = of->curve->Error(dataListH[i]);
					result += err.value;
					if (!grad.empty()) {
						int k = 0;
						for (int j = 0; j < of->curve->Parameters->Count; ++j)
						{
							if(of->curve->FittingParameters[j]) grad[k++] += 2 * err.value*err.grads[j];
						}
					}
				}
				result = result / numData;
				result = sqrt(result);
				if (!grad.empty()) {
					for (int j = 0; j < numParams; ++j)
					{
						grad[j] /= numData;
						grad[j] /= 2 * sqrt(result);
					}
				}
				if (of->cancel->IsCancellationRequested)
				{
					of->canceled = true;
					throw gcnew Exception("User Cancel");
				}

				ProgressNotification^ notification= gcnew ProgressNotification();
				notification->Count = of->count;
				notification->Error = result;
				of->progress->Report(notification);
				return result;
			}

		};

		struct ConstrainsFunction
		{


			msclr::gcroot<ApproximatingCurve^> curve;
			msclr::gcroot<List<TireData^>^> dataList;
			msclr::gcroot<System::Threading::CancellationTokenSource^> cancel;
			bool ConstrainsDependOnData;
			int numConstrains;
			int numData;

			static double Constrains(const std::vector<double> &x, std::vector<double> &grad, void *data)
			{
				auto cf= static_cast<ConstrainsFunction*>(data);
				int numParams = cf->curve->Parameters->Count;

				FuncResult result;
				if (cf->ConstrainsDependOnData)
				{
					System::Func<TireData^, FuncResult>^ func = cf->curve->ConstraintsDependOnData()[cf->numConstrains];
					List<TireData^>^ dataList = cf->dataList;
					result = func(dataList[cf->numData]);
				}
				else
				{
					System::Func<FuncResult>^ func = cf->curve->ConstraintsPure()[cf->numConstrains];
					result = func();
				}
				if (!grad.empty()) {
					for (int j = 0; j < numParams; ++j)
					{
						grad[j] = result.grads[j];
					}
				}
				/*
				if (cf->cancel->IsCancellationRequested)
				{
					throw gcnew Exception("User Cancel");
				}
				*/
				return result.value;
			}
		};

		

	};

	

	public ref class NLOptFittingSolver : public FittingSolver
	{
		NLOptFittingSolverCore* core;
	public :
		enum  class algorithm {
			GN_DIRECT = 0,
			GN_DIRECT_L,
			GN_DIRECT_L_RAND,
			GN_DIRECT_NOSCAL,
			GN_DIRECT_L_NOSCAL,
			GN_DIRECT_L_RAND_NOSCAL,
			GN_ORIG_DIRECT,
			GN_ORIG_DIRECT_L,
			GD_STOGO,
			GD_STOGO_RAND,
			LD_LBFGS_NOCEDAL,
			LD_LBFGS,
			LN_PRAXIS,
			LD_VAR1,
			LD_VAR2,
			LD_TNEWTON,
			LD_TNEWTON_RESTART,
			LD_TNEWTON_PRECOND,
			LD_TNEWTON_PRECOND_RESTART,
			GN_CRS2_LM,
			GN_MLSL,
			GD_MLSL,
			GN_MLSL_LDS,
			GD_MLSL_LDS,
			LD_MMA,
			LN_COBYLA,
			LN_NEWUOA,
			LN_NEWUOA_BOUND,
			LN_NELDERMEAD,
			LN_SBPLX,
			LN_AUGLAG,
			LD_AUGLAG,
			LN_AUGLAG_EQ,
			LD_AUGLAG_EQ,
			LN_BOBYQA,
			GN_ISRES,
			AUGLAG,
			AUGLAG_EQ,
			G_MLSL,
			G_MLSL_LDS,
			LD_SLSQP,
			LD_CCSAQ,
			GN_ESCH,
			NUM_ALGORITHMS /* not an algorithm, just the number of them */
		};
	public:
		property int Maxeval
		{
			int get() override
			{
				return core->Maxeval;
			}
				void set(int value) override
			{
				core->Maxeval = value;
			}
		}
		property int ConstrainsDependOnDataMaxeval
		{
			int get()
			{
				return core->ConstrainsDependOnDataMaxeval;
			}
			void set(int value)
			{
				core->ConstrainsDependOnDataMaxeval = value;
			}
		}
		property double Xtol
		{
			double get() override
			{
				return core->Xtol;
			}
			void set(double value) override
			{
				core->Xtol = value;
			}
		}
		property algorithm Algorithm
		{
			algorithm get()
			{
				return convert(core->Algorithm);
			}
			void set(algorithm value)
			{
				core->Algorithm = convert(value);
			}
		}

	private: 
		nlopt::algorithm convert(algorithm a)
		{
			return (nlopt::algorithm)((int)a);
		}
		algorithm convert(nlopt::algorithm a)
		{
			return (algorithm)((int)a);
		}

	public:
		
		NLOptFittingSolver()
			:core(new NLOptFittingSolverCore())
		{
		}
		~NLOptFittingSolver()
		{
			if(core != nullptr ) delete core;
		}


		int Run(
			TTCDataUtils::ApproximatingCurve^ curve,
			System::Collections::Generic::List<TTCDataUtils::TireData^>^ dataList,
			System::Threading::CancellationTokenSource^ cancel,
			System::IProgress<TTCDataUtils::ProgressNotification^>^ prg
			) override
		{
			return core->Run(curve,dataList,cancel,prg);
		}
	};
}
