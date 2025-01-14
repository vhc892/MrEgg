using UnityEngine;
using Hapiga.RemoteConfig;
using Hapiga.Ads;

[CreateAssetMenu(fileName ="Config Data")]
public class ConfigData : ScriptableObject
{
    public int inter_interval;
    public int level_show_ads;
    public bool show_AOA;

    public void GetRemoteConfig()
    {
        inter_interval = RemoteConfigManager.Instance.GetInt(RemoteConfigParamater.inter_interval);
        level_show_ads = RemoteConfigManager.Instance.GetInt(RemoteConfigParamater.level_show_ads);
        show_AOA = RemoteConfigManager.Instance.GetBool(RemoteConfigParamater.show_AOA);

        AdManager.Instance.adsConfig.inter_ads_interval_time = inter_interval;
        AdManager.Instance.adsConfig.inter_after_reward_time = inter_interval;
        AdManager.Instance.adsConfig.should_show_ads_open = show_AOA;
    }
}
public class RemoteConfigParamater
{
    public static readonly string inter_interval = "interval";
    public static readonly string level_show_ads = "level_show_ads";
    public static readonly string show_AOA = "show_AOA";
}
