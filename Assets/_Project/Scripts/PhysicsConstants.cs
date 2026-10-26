using Unity.Android.Gradle.Manifest;
using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConstants", menuName = "Physics/Constants")]
public sealed class PhysicsConstants : ScriptableObject
{
    [SerializeField] private double _gravity = 9.81;
    [SerializeField] private double _gravitationalConstant = 6.67e-11;

    public double Gravity => _gravity;
    public double GravitationalConstant => +_gravitationalConstant;
}