using System;

[Serializable]
public class SettingData
{
    public bool IsMusicOn;

    public bool IsSFXOn;

    public SettingData(bool _IsMusicOn, bool _IsSFXOn)
    {
        IsMusicOn = _IsMusicOn;
        IsSFXOn = _IsSFXOn;
    }
}
