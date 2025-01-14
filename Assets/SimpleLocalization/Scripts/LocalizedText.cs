using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization.Scripts
{
    /// <summary>
    /// Localize text component.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]

    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;

        public void Start()
        {
            //SetLocalizationKey();
            Localize();
            LocalizationManager.OnLocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.OnLocalizationChanged -= Localize;
        }

        private void Localize()
        {
            //Debug.LogError($"Localizing text: Key = {LocalizationKey}, Language = {LocalizationManager.Language}");
            GetComponent<TMP_Text>().text = LocalizationManager.Localize(LocalizationKey);
        }

        private void SetLocalizationKey()
        {
            if (GameConfig.Instance != null)
            {
                int currentLevel = (GameConfig.Instance.CurrentLevel + 1) % 30;
                if (currentLevel == 0)
                {
                    currentLevel = 30;
                }
                var textType = GetComponent<TextType>();
                if (textType != null)
                {
                    LocalizationKey = textType.GetLocalizationKey(currentLevel);
                }
                else
                {
                    Debug.LogWarning("No TextType ");
                }
            }
        }
        private void OnEnable()
        {
            GameEvents.onLevelStart += SetLocalizationKey;
            GameEvents.onLevelStart += Localize;
            //GameEvents.onLevelRestart += SetLocalizationKey;

        }

        private void OnDisable()
        {
            GameEvents.onLevelStart -= SetLocalizationKey;
            GameEvents.onLevelStart -= Localize;
            //GameEvents.onLevelRestart -= SetLocalizationKey;

        }

    }
}
