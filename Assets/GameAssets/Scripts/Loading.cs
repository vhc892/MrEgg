using DG.Tweening;
using GameAnalyticsSDK;
using Hapiga.Ads;
using Hapiga.Tracking;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.SimpleLocalization.Scripts;



public class Loading : MonoBehaviour
{
    [SerializeField] private Slider progressbar;
    [SerializeField] private Button startBtn;
    [SerializeField] private float timeCheck = 5;
    private float timer;

    private Hapiga.RemoteConfig.RemoteConfigManager RemoteConfigManager;
    private AdManager AdManager;
    IEnumerator Start()
    {
        RemoteConfigManager = Hapiga.RemoteConfig.RemoteConfigManager.Instance;
        GameAnalytics.Initialize();
        timer = 0;
        yield return new WaitUntil(() => RemoteConfigManager.isFetchSuccess || timer > timeCheck);
        AdManager = AdManager.Instance;
        timer = 0;
        GameConfig.Instance.LoadGame();
        yield return new WaitUntil(() => AdManager.IsAppOpenAdsLoaded() || timer > timeCheck);
        DoProcess(timer);
        if (GameConfig.Instance.Session > 1)
        {
            UIManager.Instance.ShowStartUI();
        }
        else
        {
            StartGame();
            CheckLanguageByCountry();
            //GameManager.Instance?.LoadLevel(0);
        }
    }
    private IEnumerator TurnOffLoading()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void ShowButton()
    {
        progressbar.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        AudioManager.Instance.MenuMusic();
        SceneManager.LoadScene("GameScene");
        StartCoroutine(TurnOffLoading());
    }
    private void DoProcess(float timer)
    {
        DG.Tweening.DOTweenModuleUI.DOValue(progressbar, 1, timer).OnComplete(() => ShowButton());
    }
    private void CheckLanguageByCountry()
    {
        SystemLanguage systemLanguage = Application.systemLanguage;
        //SystemLanguage systemLanguage = SystemLanguage.Russian;
        string selectedLanguage;

        switch (systemLanguage)
        {
            case SystemLanguage.English:
                selectedLanguage = "English";
                break;
            case SystemLanguage.Russian:
                selectedLanguage = "Russian";
                break;
            case SystemLanguage.German:
                selectedLanguage = "German";
                break;
            case SystemLanguage.French:
                selectedLanguage = "French";
                break;
            case SystemLanguage.Portuguese:
                selectedLanguage = "Brazil";
                break;
            default:
                selectedLanguage = "English";
                break;
        }
        GameConfig.Instance.Language(selectedLanguage);
    }



}
