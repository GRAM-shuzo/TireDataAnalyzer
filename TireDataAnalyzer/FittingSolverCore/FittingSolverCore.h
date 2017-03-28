#pragma once




namespace MagicFormulaFittingSolver
{
	public ref class FittingSolver abstract
	{
	public:
		property virtual int Maxeval
		{
			
			int get() = 0;
			void set(int value) = 0;
		}
		property virtual double Xtol
		{
			double get() = 0;
			void set(double value) = 0;
		}

		virtual int Run(
			TTCDataUtils::ApproximatingCurve^ curve,
			System::Collections::Generic::List<TTCDataUtils::TireData^>^ dataList,
			System::Threading::CancellationTokenSource^ cancel,
			System::IProgress<TTCDataUtils::ProgressNotification^>^ prg
			) = 0;
	};

	public ref class NoFitting
		: public FittingSolver
	{
	public:
		property int Maxeval
		{
			int get() override
			{
				return 200;
			}
			void set(int value) override
			{
			}
		}
		property double Xtol
		{
			double get() override
			{
				return 1;
			}
			void set(double value) override
			{
			}
		}

		int Run(
			TTCDataUtils::ApproximatingCurve^ curve,
			System::Collections::Generic::List<TTCDataUtils::TireData^>^ dataList,
			System::Threading::CancellationTokenSource^ cancel,
			System::IProgress<TTCDataUtils::ProgressNotification^>^ prg
			) override
		{
			for (int i = 0; i < 200; ++i)
			{
				System::Threading::Thread::Sleep(20);
				TTCDataUtils::ProgressNotification^ pn = gcnew TTCDataUtils::ProgressNotification();
				pn->Count = i;
				pn->Error = 201-i;
				prg->Report(pn);
				if (cancel->IsCancellationRequested)
				{
					throw gcnew System::OperationCanceledException("User Cancel");
				}
			}
			
			return 0;
		}
	};
}