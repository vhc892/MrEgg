using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextType : MonoBehaviour
{
    public enum Text
    {
        TagLine,
        Hint
    }
    public Text TextCategory;

    public string GetLocalizationKey(int currentLevel)
    {
        switch (TextCategory)
        {
            case Text.TagLine:
                return $"tagline_lv{currentLevel}";
            case Text.Hint:
                return $"hint_lv{currentLevel}";
            default:
                Debug.LogWarning("Error");
                return string.Empty;
        }
    }
}
