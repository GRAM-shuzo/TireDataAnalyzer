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
		int MaxDataUsage = 10000;
	public:
		int Run(TTCDataUtils::ApproximatingCurve^ curve, List<TireData^>^ dataList, CancellationTokenSource^ cancel, IProgress<TTCDataUtils::ProgressNotification^>^ prg)
		{
			int numParameters = 0;
			for (int i = 0; i < curve->Parameters->Count; ++i)
			{
				if (curve->FittingParameters[i]) ++numParameters;
			}
			VectorXd p(numParameters);
			GetListDouble(p, curve->Parameters, curve->FittingParameters);

			ObjFunctor functor(curve, dataList, this, cancel, prg);
			LevenbergMarquardt<ObjFunctor> lm(functor);
			lm.parameters.xtol = Xtol;
			lm.parameters.maxfev = Maxeval;
			int r = lm.minimize(p);
			SetListDouble(curve->Parameters, p, curve->FittingParameters);
			return r;
		}

		static void GetListDouble(VectorXd& p, List<double>^ params, List<bool>^ enable)
		{
			int j = 0;
			for (int i = 0; i < params->Count; ++i)
			{
				if (enable[i])
				{
					double a = params[i];
					p[j++] = a;
				}
			}
		}
		static void SetListDouble(List<double>^ params, const VectorXd& p, List<bool>^ enable)
		{
			int j = 0;
			for (int i = 0; i < params->Count; ++i)
			{
				if (enable[i])
				{
					double a = p[j++];
					params[i] = a;
				}
			}
		}
		

		struct ObjFunctor
		{
			ObjFunctor( ApproximatingCurve^ curve_, List<TireData^>^ dataList_, LMFittingSolverCore* c, CancellationTokenSource^ can, IProgress<TTCDataUtils::ProgressNotification^>^ prg)
				: curve(curve_), dataList(dataList_), lmfsc(c), cancel(can),progress(prg)
			{
				DataPoints = min(dataList->Count, lmfsc->MaxDataUsage);
			}

			int DataPoints;
			msclr::gcroot<ApproximatingCurve^> curve;
			msclr::gcroot<List<TireData^>^> dataList;
			msclr::gcroot<CancellationTokenSource^> cancel;
			msclr::gcroot<IProgress<TTCDataUtils::ProgressNotification^>^> progress;
			LMFittingSolverCore* lmfsc;
			int count = 0;
			// 目的関数
			int operator()(const VectorXd& b, VectorXd& fvec)
			{
				SetListDouble(curve->Parameters, b, curve->FittingParameters);
				int i = 0;
				for (; i < DataPoints; ++i)
				{
					List<TireData^>^ list = dataList;
					auto result = curve->Error(list[i]);
					fvec[i] = result.value;
				}
				/*for (i = DataPoints; i < (DataPoints)*(curve->ConstraintsDependOnData()->Count + 1); ++i)
				{
					int j = (i / DataPoints)-1;
					List<TireData^>^ list = dataList;
					auto result = curve->ConstraintsDependOnData()[j](list[i%DataPoints]);
					fvec[i] = pow(result.value,-lmfsc->ConstrainsDim);
				}
				for (int j = 0; i < (curve->ConstraintsPure()->Count); ++j)
				{
					auto result = curve->ConstraintsPure()[j]();
					fvec[i+j] = pow(result.value, -lmfsc->ConstrainsDim);
				}*/
				if (cancel->IsCancellationRequested)
				{
					throw gcnew OperationCanceledException("User Cancel");
				}

				ProgressNotification^ notification = gcnew ProgressNotification();
				notification->Count = ++count;
				notification->Error = CulcError(fvec);
				progress->Report(notification);

				return 0;
			}

			double CulcError(const VectorXd& fvec)
			{
				double e = 0;
				for (int i =0; i < DataPoints; ++i)
				{ 
					e += fvec[i] * fvec[i];
				}
				e /= DataPoints;
				e = sqrt(e);
				return e;
			}

			// 微分,ヤコビアン
			int df(const VectorXd& b, MatrixXd& fjac)
			{

				SetListDouble(curve->Parameters, b, curve->FittingParameters);

				int i = 0;
				for (; i < DataPoints; ++i)
				{
					List<TireData^>^ list = dataList;
					auto result = curve->Error(list[i]);
					int k = 0;
					for (int j = 0; j < curve->Parameters->Count; ++j)
					{
						if (curve->FittingParameters[j]) fjac(i, k++) = result.grads[j];
					}
				}
				/*for (i = DataPoints; i < (DataPoints)*(curve->ConstraintsDependOnData()->Count + 1); ++i)
				{
					int j = (i / DataPoints) - 1;
					List<TireData^>^ list = dataList;
					auto result = curve->ConstraintsDependOnData()[j](list[i%DataPoints]);

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
				}*/
				if (cancel->IsCancellationRequested)
				{
					throw gcnew Exception("User Cancel");
				}
				return 0;
			}
			int inputs() const 
			{ 
				int numParameters = 0;
				for (int i = 0; i < curve->Parameters->Count; ++i)
				{
					if (curve->FittingParameters[i]) ++numParameters;
				}
				return numParameters;
			}
			int values() const { return (DataPoints)*(curve->ConstraintsDependOnData()->Count+1)+(curve->ConstraintsPure()->Count); }
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
		property double MaxDataUsage
		{
			double get() override
			{
				return core->MaxDataUsage;
			}
			void set(double value) override
			{
				core->MaxDataUsage = value;
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