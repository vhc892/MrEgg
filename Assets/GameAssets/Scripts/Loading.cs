using GameAnalyticsSDK;
using Hapiga.Ads;
using Hapiga.Tracking;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public float timeCheck = 5;
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

        yield return new WaitUntil(() => AdManager.IsAppOpenAdsLoaded() || timer > timeCheck);
        SceneManager.LoadScene("Menu");
        //TrackingManager.TrackEvent(FirebaseParameter.LOADED);

    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
}
