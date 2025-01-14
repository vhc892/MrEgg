using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    Music,
    SFX
}
public class ToggleUI : MonoBehaviour
{
    [SerializeField] Sprite uiOn;
    [SerializeField] Sprite uiOff;
    [SerializeField] SoundType soundType;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnStart()
    {
        if (soundType == SoundType.Music)
            image.sprite = GameConfig.Instance.IsMusicOn ? uiOn : uiOff;
        else
            image.sprite = GameConfig.Instance.IsSFXOn ? uiOn : uiOff;
    }

    public void Toggle()
    {
        image.sprite = image.sprite == uiOn ? uiOff : uiOn;
        AudioManager.Instance.PlaySFX("SelectButton");
    }

    public void ToggleMusic()
    {
        GameConfig.Instance.ChangeMusicState();
    }
    public void ToggleSFX()
    {
        GameConfig.Instance.ChangeSFXState();
    }
}
