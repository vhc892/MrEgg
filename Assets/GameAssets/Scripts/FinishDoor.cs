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
    Collider2D col;
    Rigidbody2D rb;
    DoorChild doorChild;

    bool isFinished;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        doorChild = GetComponentInChildren<DoorChild>();
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
            UIManager.Instance.ingameUI.LevelCompleted();
        }
        else 
        {
            GameConfig.Instance.CurrentLevel = 0;
            GameConfig.Instance.LevelPass = GameConfig.Instance.CurrentLevel;
            UIManager.Instance.ingameUI.LevelCompleted();
        }
    }

    public void OnFinish()
    {
        if (!unlock) return;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        col.isTrigger = true;
        EndLevel();
        DoorSwitch();
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && unlock)
    //    {
    //        transform.position = transform.position;
    //        DoorSwitch();
    //        EndLevel();
    //    }
    //}
}
