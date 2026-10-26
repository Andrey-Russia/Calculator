using System;

public interface IFormula
{
    int InputCount { get; }

    double Calculate(double[] values);
}

public sealed class SpeedFormula : IFormula
{
    public int InputCount => 2;

    public double Calculate(double[] values)
    {
        return PhysicsFormulas.CalculateSpeed(values[0], values[1]);
    }
}

public sealed class AccelerationFormula : IFormula
{
    public int InputCount => 3;

    public double Calculate(double[] values)
    {
        return PhysicsFormulas.CalculateAcceleration(values[0], values[1], values[2]);
    }
}

public sealed class FreeFallFormula : IFormula
{
    public int InputCount => 1;

    public double Calculate(double[] values)
    {
        return PhysicsFormulas.CalculateFreeFallHeight(values[0]);
    }
}

public sealed class AcceleratedWeightFormula : IFormula
{
    public int InputCount => 2;

    public double Calculate(double[] values)
    {
        return PhysicsFormulas.CalculateAcceleratedWeight(values[0], values[1]);
    }
}

public sealed class GravitationFormula : IFormula
{
    public int InputCount => 3;

    public double Calculate(double[] values)
    {
        return PhysicsFormulas.CalculateGravitation(values[0], values[1], values[2]);
    }
}

public static class FormulaFactory
{
    public static IFormula Create(FormulaType type)
    {
        switch (type)
        {
            case FormulaType.Speed:
                return new SpeedFormula();

            case FormulaType.Acceleration:
                return new AccelerationFormula();

            case FormulaType.FreeFallHeight:
                return new FreeFallFormula();

            case FormulaType.AcceleratedWeight:
                return new AcceleratedWeightFormula();

            case FormulaType.Gravitation:
                return new GravitationFormula();

            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}