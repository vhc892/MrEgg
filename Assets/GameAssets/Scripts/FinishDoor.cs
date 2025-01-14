using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FinishDoor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool unlock;
    [SerializeField] private bool clickToUnlock;
    [SerializeField] private GameObject lockImage;
    private float clickCountToOpen = 3;

    Animator anim;
    Collider2D col;
    Rigidbody2D rb;
    DoorChild doorChild;

    bool isFinished;

    public bool Unlock { get => unlock; set => unlock = value; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        doorChild = GetComponentInChildren<DoorChild>();
    }

    public void OnStart()
    {
        if (!unlock)
        {
            //lockImage.SetActive(true);
            doorChild.col.enabled = false;
        }
        else
            doorChild.enabled = true;
    }

    public void UnlockDoor()
    {
        unlock = true;
        if(lockImage != null)
        {
            lockImage.gameObject.SetActive(false);
        }
        doorChild.col.enabled = true;
        AudioManager.Instance.PlaySFX("OpenDoor");
        anim?.Play("DoorOpen");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickToUnlock)
        {
            if (clickCountToOpen > 0)
            {
                AudioManager.Instance.PlaySFXOneShot("DoorKnock");
            }
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
        anim?.Play("DoorOpen");
    }

    public void EndLevel()
    {
        GameEvents.LevelFinish();
    }

    public void OnFinish()
    {
        if (!unlock) return;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        col.isTrigger = true;
        DoorSwitch();
        EndLevel();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lock")
        {
            unlock = true;
            doorChild.col.enabled = true;
        }
    }

    private void OnEnable()
    {
        GameEvents.onLevelRestart += OnStart;
        GameEvents.onDoorUnlocked += UnlockDoor;
        GameEvents.onLevelStart += OnStart;
    }

    private void OnDisable()
    {
        GameEvents.onLevelRestart -= OnStart;
        GameEvents.onDoorUnlocked -= UnlockDoor;
        GameEvents.onLevelStart -= OnStart;
    }
}
