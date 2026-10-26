using UnityEngine;

public enum FormulaType
{
    Speed,
    Acceleration,
    FreeFallHeight,
    AcceleratedWeight,
    Gravitation
}

[CreateAssetMenu(fileName = "FormulaViewData", menuName = "Physics/Formula View Data")]
public sealed class FormulaViewData : ScriptableObject
{
    [SerializeField] private FormulaType _formulaType;

    [SerializeField] private string _title;

    [SerializeField, TextArea] private string _formulaText;

    [SerializeField] private string _resultUnit;

    [SerializeField] private string[] _inputDescriptions;

    public FormulaType Type => _formulaType;
    public string Title => _title;
    public string FormulaText => _formulaText;
    public string ResultUnit => _resultUnit;

    public int InputCount => _inputDescriptions.Length;

    public string GetInputDescription(int index)
    {
        return _inputDescriptions[index];
    }
}