public class Calculator
{
    public static double RationalFunctionCalculator(int x, int n)
    {
        if (x == 0 || n < 0) return -1;
        int power = 1;
        for (int i = 0; i < n; i++)
            power *= x;
        return 1.0 / power;
    }

    public static double FunctionCalculator(double x)
    {
        if (x == 0)
            return -1;
        return 1.0 / x;
    }
}
