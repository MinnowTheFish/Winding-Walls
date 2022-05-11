using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class Settings : MonoBehaviour
{
    public Slider S_SensitivityX, S_SensitivityY;
    public InputField IF_SensitivityX, IF_SensitivityY;
    
    void Start() {
        IF_SensitivityX.text = GlobalSettings.MouseSensitivity.x.ToString(CultureInfo.InvariantCulture);
        S_SensitivityX.value = GlobalSettings.MouseSensitivity.x;
        IF_SensitivityY.text = GlobalSettings.MouseSensitivity.y.ToString(CultureInfo.InvariantCulture);
        S_SensitivityY.value = GlobalSettings.MouseSensitivity.y;
    }

    public void EnableCanvas(Canvas c) {
        c.gameObject.SetActive(true);
    }

    public void DisableCanvas(Canvas c) {
        c.gameObject.SetActive(false);
    }

    public void ChangeSensitivity(string type)
    {
        float input = float.NegativeInfinity;
        switch (type) {
            case "S_SensitivityX":
                input = S_SensitivityX.value;
                break;
            case "IF_SensitivityX":
                input = float.Parse(IF_SensitivityX.text, CultureInfo.InvariantCulture);
                break;
            case "S_SensitivityY":
                input = S_SensitivityY.value;
                break;
            case "IF_SensitivityY":
                input = float.Parse(IF_SensitivityY.text, CultureInfo.InvariantCulture);
                break;
        }
        if (float.IsNegativeInfinity(input))
            return;
        input = Mathf.Clamp(input, 0.1f, 10);
        switch (type) {
            case "S_SensitivityX":
            case "IF_SensitivityX":
                IF_SensitivityX.text = input.ToString(CultureInfo.InvariantCulture);
                S_SensitivityX.value = input;
                GlobalSettings.MouseSensitivity.x = input;
                break;
            case "S_SensitivityY":
            case "IF_SensitivityY":
                IF_SensitivityY.text = input.ToString(CultureInfo.InvariantCulture);
                S_SensitivityY.value = input;
                GlobalSettings.MouseSensitivity.y = input;
                break;
        }
    }
}
