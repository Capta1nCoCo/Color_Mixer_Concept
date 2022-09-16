using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ComplianceDisplay : MonoBehaviour
{
    private float delayInSeconds = 1f;

    private string startComplianceValue = "0%";
    private string fullComplianceValue = "100%";

    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = startComplianceValue;
        GameEvents.ChangeCurrentComplinceValue += OnChangeCurrentComplinceValue;
    }

    private void OnDestroy()
    {
        GameEvents.ChangeCurrentComplinceValue -= OnChangeCurrentComplinceValue;
    }

    private void OnChangeCurrentComplinceValue(string value)
    {
        textMeshProUGUI.text = value;
        if (value == fullComplianceValue)
        {
            StartCoroutine(CompleteLevelWithDelay());
        }
    }

    private IEnumerator CompleteLevelWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        GameEvents.LevelCompleted();
    }
}
