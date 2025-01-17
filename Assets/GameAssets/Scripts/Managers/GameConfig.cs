using System.Collections;
using UnityEngine;
using Assets.SimpleLocalization.Scripts;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        LocalizationManager.Read();

        //switch (Application.systemLanguage)
        //{
        //    case SystemLanguage.English:
        //        LocalizationManager.Language = "English";
        //        break;
        //    case SystemLanguage.Russian:
        //        LocalizationManager.Language = "Russian";
        //        break;
        //    case SystemLanguage.German:
        //        LocalizationManager.Language = "German";
        //        break;
        //    case SystemLanguage.French:
        //        LocalizationManager.Language = "French";
        //        break;
        //    case SystemLanguage.Portuguese:
        //        LocalizationManager.Language = "Brazil";
        //        break;
        //}
    }

    public GameData gameData;
    public ConfigData configData;

    public void LoadGame()
    {
#if !UNITY_EDITOR
        Application.targetFrameRate = 60;
        Debug.unityLogger.logEnabled = false;
#endif
        StartCoroutine(LoadGameCoroutine());
        configData.GetRemoteConfig();
    }

    IEnumerator LoadGameCoroutine()
    {
        LoadData();
        yield return null;
    }

    public int LevelPass { get { return gameData.LevelPass; } set { gameData.LevelPass = value; } }
    public int CurrentLevel { get { return gameData.CurrentLevel; } set { gameData.CurrentLevel = value; GameEvents.LevelIndexChange(); } }
    public int LightBulb { get { return gameData.LightBulb; } set { gameData.LightBulb = value; GameEvents.LightBulbChange(); } }
    public int Session { get { return gameData.Session; } }

    [HideInInspector] public bool IsMusicOn;
    [HideInInspector] public bool IsSFXOn;

    void LoadData()
    {
        IsMusicOn = true;
        IsSFXOn = true;

        SettingData settingData = SaveSystemData.LoadSettingsData();
        if (settingData != null)
        {
            IsMusicOn = settingData.IsMusicOn;
            IsSFXOn = settingData.IsSFXOn;
        }
        string language = SaveSystemData.LoadLanguage();
        LocalizationManager.Language = language;

        gameData = new GameData();
        PlayerData playerData = SaveSystemData.LoadPlayerData();

        if (playerData != null)
        {
            gameData.LevelPass = playerData.levelPass;
            gameData.CurrentLevel = playerData.currentLevel;
            gameData.LightBulb = playerData.lightBulb;
            gameData.Session = playerData.session;
        }
        else
        {
            gameData.LevelPass = 0;
            gameData.CurrentLevel = 0;
            gameData.LightBulb = 0;
            gameData.Session = 0;
        }
        gameData.Session++;
    }

    public void ChangeMusicState()
    {
        IsMusicOn = !IsMusicOn;
        SaveSystemData.SaveSettingsData(IsMusicOn, IsSFXOn);
        AudioManager.Instance.ToggleMusic();
    }

    public void ChangeSFXState()
    {
        IsSFXOn = !IsSFXOn;
        SaveSystemData.SaveSettingsData(IsMusicOn, IsSFXOn);
        AudioManager.Instance.ToggleSFX();
    }
    public void Language(string language)
    {
        LocalizationManager.Language = language;
        SaveSystemData.SaveLanguage(language);
        UIManager.Instance.ingameUI.LanguageClose();
    }
}
