using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public enum CalibrationScaling
	{
		None,
		Inverse,
		InverseSquare,
		Logarithm,
		InverseLogarithm
	}

    public static class CalibrationScalingOperations
    {
        public static double ApplyScaling(this CalibrationScaling scaling, double amount)
        {
            switch (scaling)
            {
                case CalibrationScaling.None:
                    return amount;
                case CalibrationScaling.Inverse:
                    return 1.0 / amount;
                case CalibrationScaling.InverseSquare:
                    return 1.0 / (amount * amount);
                case CalibrationScaling.Logarithm:
                    return Math.Log10(amount);
                case CalibrationScaling.InverseLogarithm:
                    return 1.0 / Math.Log10(amount);
                default:
                    throw new ArgumentOutOfRangeException(nameof(scaling), scaling, null);
            }
        }
    }
}