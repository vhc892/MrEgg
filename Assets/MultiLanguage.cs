using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public class MultiLanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                LocalizationManager.Language = "English";
                break;
            case SystemLanguage.Russian:
                LocalizationManager.Language = "Russian";
                break;
            case SystemLanguage.German:
                LocalizationManager.Language = "German";
                break;
            case SystemLanguage.French:
                LocalizationManager.Language = "French";
                break;
            case SystemLanguage.Portuguese:
                LocalizationManager.Language = "Brazil";
                break ;
        }
    }
    public void Language(string language)
    {
        AudioManager.Instance.PlaySFX("SelectButton");
        LocalizationManager.Language = language;
    }
}
