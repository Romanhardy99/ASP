using System.Numerics;

namespace Blazor.Components.Pages
{
    public partial class Fibonacci
    {
        private const int MaxCount = 200;
        private int count = 10;
        private string? error;
        private readonly List<BigInteger> numbers = new();

        protected override void OnInitialized() => Calculate();

        private void Calculate()
        {
            error = null;
            numbers.Clear();

            if(count < 1)
            {
                error = "Количество должно быть не меньше 1.";
                return;
            }

            if (count > MaxCount)
            {
                error = $"Максимум {MaxCount} чисел - иначе страница станет нечитаемой.";
                return;
            }

            BigInteger a = 0, b = 1;
            for (int i = 0; i < count; i++)
            {
                numbers.Add(a);
                (a, b) = (b, a + b);
            }
        }

        private BigInteger Total =>
            numbers.Aggregate(BigInteger.Zero, (acc, x) => acc + x);
    }
}
