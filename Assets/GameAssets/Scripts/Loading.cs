using DG.Tweening;
using GameAnalyticsSDK;
using Hapiga.Ads;
using Hapiga.Tracking;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        if (GameConfig.Instance.Session > 1)
        {
            UIManager.Instance.ShowStartUI();
        }
        else
        {
            StartGame();
            //GameManager.Instance?.LoadLevel(0);
        }
        yield return new WaitUntil(() => AdManager.IsAppOpenAdsLoaded() || timer > timeCheck);
        DoProcess(timer);
        //TrackingManager.TrackEvent(FirebaseParameter.LOADED);

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


}
