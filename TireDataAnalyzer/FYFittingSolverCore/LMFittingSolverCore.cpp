#include "stdafx.h"
#include <vector>
#include <algorithm>
#include <functional>
#include <random>
#include <chrono> 
#include "Eigen/Dense"
#include "unsupported/Eigen/NonLinearOptimization"
#include <msclr/gcroot.h>
#include "FittingSolverCore.h"
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
using namespace msclr;
using namespace System;
using namespace System::Collections::Generic;
using namespace TTCDataUtils;
using namespace System::IO;
using namespace System::Text;
using namespace Eigen;
using namespace std;
using namespace System::Threading;

namespace MagicFormulaFittingSolver
{
	struct LMFittingSolverCore
	{
	public:
		int ConstrainsDim = 10;
		int Maxeval;
		int Xtol;
	public:
		int Run(TTCDataUtils::ApproximatingCurve^ curve, List<TireData^>^ dataList, CancellationTokenSource^ cancel, IProgress<TTCDataUtils::ProgressNotification^>^ prg)
		{
			int numParameters = curve->Parameters->Count;
			VectorXd p(numParameters);
			GetListDouble(p, curve->Parameters);

			ObjFunctor functor(curve, dataList, this, cancel, prg);
			LevenbergMarquardt<ObjFunctor> lm(functor);
			int r = lm.minimize(p);
			return r;
		}
		static void GetListDouble(VectorXd& p, List<double>^ params)
		{
			for (int i = 0; i < params->Count; ++i)
			{
				p[i] = params[i];
			}
		}
		static void SetListDouble(List<double>^ params, const VectorXd& p)
		{
			for (int i = 0; i < params->Count; ++i)
			{
				params[i] = p[i];
			}
		}

		struct ObjFunctor
		{
			ObjFunctor( ApproximatingCurve^ curve_, List<TireData^>^ dataList_, LMFittingSolverCore* c, CancellationTokenSource^ can, IProgress<TTCDataUtils::ProgressNotification^>^ prg)
				: curve(curve_), dataList(dataList_), lmfsc(c), cancel(can),progress(prg){}

			msclr::gcroot<ApproximatingCurve^> curve;
			msclr::gcroot<List<TireData^>^> dataList;
			msclr::gcroot<CancellationTokenSource^> cancel;
			msclr::gcroot<IProgress<TTCDataUtils::ProgressNotification^>^> progress;
			LMFittingSolverCore* lmfsc;
			// 目的関数
			int operator()(const VectorXd& b, VectorXd& fvec) const
			{
				int i = 0;
				for (; i < dataList->Count; ++i)
				{
					List<TireData^>^ list = dataList;
					auto result = curve->Error(list[i]);
					fvec[i] = result.value;
				}
				for (i = dataList->Count; i < (dataList->Count)*(curve->ConstraintsDependOnData()->Count + 1); ++i)
				{
					int j = (i / dataList->Count)-1;
					List<TireData^>^ list = dataList;
					auto result = curve->ConstraintsDependOnData()[j](list[i%dataList->Count]);
					fvec[i] = pow(result.value,-lmfsc->ConstrainsDim);
				}
				for (int j = 0; i < (curve->ConstraintsPure()->Count); ++j)
				{
					auto result = curve->ConstraintsPure()[j]();
					fvec[i+j] = pow(result.value, -lmfsc->ConstrainsDim);
				}
				if (cancel->IsCancellationRequested)
				{
					throw gcnew OperationCanceledException("User Cancel");
				}
				return 0;
			}
			// 微分,ヤコビアン
			int df(const VectorXd& b, MatrixXd& fjac)
			{
				int i = 0;
				for (; i < dataList->Count; ++i)
				{
					List<TireData^>^ list = dataList;
					auto result = curve->Error(list[i]);

					for (int j = 0; j < inputs(); ++j)
					{
						fjac(i, j) = result.grads[j];
					}
				}
				for (i = dataList->Count; i < (dataList->Count)*(curve->ConstraintsDependOnData()->Count + 1); ++i)
				{
					int j = (i / dataList->Count) - 1;
					List<TireData^>^ list = dataList;
					auto result = curve->ConstraintsDependOnData()[j](list[i%dataList->Count]);

					for (int j = 0; j < inputs(); ++j)
					{
						fjac(i, j) = result.grads[j];
					}
				}
				for (int j = 0; i < (curve->ConstraintsPure()->Count); ++j)
				{
					auto result = curve->ConstraintsPure()[j]();
					for (int j = 0; j < inputs(); ++j)
					{
						fjac(i, j) = -lmfsc->ConstrainsDim*pow(result.value, -lmfsc->ConstrainsDim-1)*result.grads[j];
					}
				}
				if (cancel->IsCancellationRequested)
				{
					throw gcnew Exception("User Cancel");
				}
				return 0;
			}
			int inputs() const { return curve->Parameters->Count; }
			int values() const { return (dataList->Count)*(curve->ConstraintsDependOnData()->Count+1)+(curve->ConstraintsPure()->Count); }
		};


	};

	public ref class LMFittingSolver : public MagicFormulaFittingSolver::FittingSolver
	{
	public:
		LMFittingSolverCore* core;
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

	public:

		LMFittingSolver()
			:core(new LMFittingSolverCore())
		{
		}
		~LMFittingSolver()
		{
			if (core != nullptr) delete core;
		}


		int Run(
			TTCDataUtils::ApproximatingCurve^ curve,
			System::Collections::Generic::List<TTCDataUtils::TireData^>^ dataList,
			System::Threading::CancellationTokenSource^ cancel,
			System::IProgress<TTCDataUtils::ProgressNotification^>^ prg
			) override
		{
			return core->Run(curve, dataList, cancel, prg);
		}
	};
}