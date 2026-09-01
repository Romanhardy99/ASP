using System.Collections.Specialized;

namespace Blazor.Components.Pages
{
    public partial class Power
    {
        private double baseValue = 2;
        private int exponent = 10;
        private double? result;
        private string? error;

        private void Calculate()
        {
            error = null;
            result = null;

            if (baseValue == 0 && exponent < 0)
            {
                error = "ноль нельзя возводить в отрицательную степень.";
                return;
            }

            result = IntPow(baseValue, exponent);
        }

        private static double IntPow(double b, int e)
        {
            if (e < 0) return 1 / IntPow(b, -e);
            double acc = 1;
            for(int i = 0; i < e; i++)
                acc *= b;

            return acc;
        }
    }
}
