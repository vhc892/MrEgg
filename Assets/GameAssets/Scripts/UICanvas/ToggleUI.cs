using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] GameObject uiOn;
    public void Toggle(bool isOn)
    {
        isOn = !isOn;
        uiOn.SetActive(isOn);
    }
}
