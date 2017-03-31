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
			
			
			return 0;
		}
	};
}