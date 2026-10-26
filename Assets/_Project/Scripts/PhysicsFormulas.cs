using System;
using UnityEngine;

public static class PhysicsFormulas
{
    private static PhysicsConstants constants;

    private static PhysicsConstants Constants
    {
        get
        {
            if (constants == null)
            {
                constants = Resources.Load<PhysicsConstants>("Physics/PhysicsConstants");
            }

            return constants;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetCache()
    {
        constants = null;
    }

    public static double CalculateSpeed(double distance, double time)
    {
        RequireNonNegative(distance, "Путь");
        RequirePositive(time, "Время");

        return RequireFinite(distance / time);
    }

    public static double CalculateAcceleration(double finalVelocity, double initialVelocity, double time)
    {
        RequireFinite(finalVelocity);
        RequireFinite(initialVelocity);
        RequirePositive(time, "Время");

        return RequireFinite((finalVelocity - initialVelocity) / time);
    }

    public static double CalculateFreeFallHeight(double time)
    {
        RequireNonNegative(time, "Время");

        double gravity = Constants.Gravity;
        RequirePositive(gravity, "Константа g");

        return RequireFinite(gravity * time * time / 2.0);
    }

    public static double CalculateAcceleratedWeight(double mass, double acceleration)
    {
        RequireNonNegative(mass, "Масса");
        RequireNonNegative(acceleration, "Ускорение вверх");

        double gravity = Constants.Gravity;
        RequirePositive(gravity, "Константа g");

        return RequireFinite(mass * (gravity + acceleration));
    }

    public static double CalculateGravitation(double firstMass, double secondMass, double distance)
    {
        RequireNonNegative(firstMass, "Первая масса");
        RequireNonNegative(secondMass, "Вторая масса");
        RequirePositive(distance, "Расстояние");

        double gravitationalConstant = Constants.GravitationalConstant;
        RequirePositive(gravitationalConstant, "Константа G");

        return RequireFinite(gravitationalConstant * firstMass * secondMass / (distance * distance));
    }

    private static void RequirePositive(double value, string name)
    {
        RequireFinite(value);

        if (value <= 0)
        {
            throw new ArgumentException($"{name}: значение должно быть > 0.");
        }
    }

    private static void RequireNonNegative(double value, string name)
    {
        RequireFinite(value);

        if (value < 0)
        {
            throw new ArgumentException($"{name}: значение должно быть ≥ 0.");
        }
    }

    private static double RequireFinite(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            throw new ArgumentException("Число выходит за допустимый диапазон.");
        }

        return value;
    }
}