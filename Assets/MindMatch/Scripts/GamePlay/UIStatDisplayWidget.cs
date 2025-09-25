using UnityEngine;
using TMPro;

public class UIStatDisplayWidget : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TextMeshProUGUI _valueText;

    public void Set(string label, string value)
    {
        _labelText.text = label;

        _valueText.text = value;
    }
    public void SetValue(string value)
    {
        _valueText.text = value;
    }
    public void SetLabel(string label)
    {
        _labelText.text = label;
    }
    public void SetValue(int value)
    {
        SetValue(value.ToString());
    }

    public void SetValue(float value, string format = "0.0")
    {
        SetValue(value.ToString(format));
    }
}
