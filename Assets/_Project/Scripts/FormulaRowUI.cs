using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class FormulaRowUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FormulaViewData viewData;

    [Header("Formula")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text formulaText;

    [Header("Inputs")]
    [SerializeField] private TMP_Text[] inputLabels;
    [SerializeField] private TMP_InputField[] inputFields;

    [Header("Result")]
    [SerializeField] private Button calculateButton;
    [SerializeField] private TMP_Text resultText;

    private IFormula formula;
    private double[] values;

    private void Awake()
    {
        if (viewData == null)
        {
            Debug.LogError("Не назначен FormulaViewData.", this);
            enabled = false;
            return;
        }

        formula = FormulaFactory.Create(viewData.Type);

        if (inputFields.Length != formula.InputCount || inputLabels.Length != formula.InputCount || viewData.InputCount != formula.InputCount)
        {
            Debug.LogError("Количество инпутов и подписей не соответствует формуле.",this);

            calculateButton.interactable = false;
            enabled = false;
            return;
        }

        values = new double[formula.InputCount];

        titleText.text = viewData.Title;
        formulaText.text = viewData.FormulaText;

        for (int i = 0; i < inputFields.Length; i++)
        {
            inputLabels[i].text = viewData.GetInputDescription(i);
        }
    }

    private void OnEnable()
    {
        if (formula == null || values == null)
            return;

        foreach (TMP_InputField input in inputFields)
        {
            input.onValueChanged.AddListener(OnInputChanged);
        }

        calculateButton.onClick.AddListener(Calculate);

        UpdateButtonState();
    }

    private void OnDisable()
    {
        foreach (TMP_InputField input in inputFields)
        {
            if (input != null)
            {
                input.onValueChanged.RemoveListener(OnInputChanged);
            }
        }

        if (calculateButton != null)
        {
            calculateButton.onClick.RemoveListener(Calculate);
        }
    }

    private void OnInputChanged(string unused)
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        resultText.text = "—";
        calculateButton.interactable = TryReadValues();
    }

    private bool TryReadValues()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            string text = inputFields[i].text.Trim().Replace(',', '.');

            bool parsed = double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out values[i]);

            if (!parsed || double.IsNaN(values[i]) || double.IsInfinity(values[i]))
            {
                return false;
            }
        }

        return true;
    }

    private void Calculate()
    {
        if (!TryReadValues())
        {
            calculateButton.interactable = false;
            return;
        }

        try
        {
            double result = formula.Calculate(values);

            resultText.text =result.ToString( "G8", CultureInfo.GetCultureInfo("ru-RU")) + " " + viewData.ResultUnit;
        }
        catch (ArgumentException exception)
        {
            resultText.text = exception.Message;
        }
    }
}