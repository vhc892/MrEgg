using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;


public class FinishDoor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool unlock;
    [SerializeField] private bool clickToUnlock;
    [SerializeField] private SpriteRenderer lockImage;
    private float clickCountToOpen = 3;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void UnlockDoor()
    {
        unlock = true;
        if(lockImage != null)
        {
            lockImage.gameObject.SetActive(false);
        }
        anim?.Play("DoorOpen");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickToUnlock)
        {
            --clickCountToOpen;
            var knockEffect = GetComponentInChildren<KnockEffect>();
            if (!unlock && knockEffect != null)
            {
                knockEffect.PlayKnockAnimation();
            }
            if (clickCountToOpen == 0)
            {
                UnlockDoor();
            }
        }
    }

    public void DoorSwitch()
    {
        GameEvents.LevelFinish();
        anim?.Play("DoorOpen");
    }

    public void EndLevel()
    {
        if (GameConfig.Instance.LevelPass > GameConfig.Instance.CurrentLevel)
        {
            GameManager.Instance.LevelCompleted();
        }
        else 
        {
            GameConfig.Instance.LevelPass = GameConfig.Instance.CurrentLevel;
            GameManager.Instance.LevelCompleted();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && unlock)
        {
            transform.position = transform.position;
            DoorSwitch();
            EndLevel();
        }
    }
}
