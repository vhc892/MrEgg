using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FinishDoor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool unlock;
    [SerializeField] private bool clickToUnlock;
    [SerializeField] private SpriteRenderer lockImage;
    private SpriteRenderer doorSprite;
    private float clickCountToOpen = 3;


    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
    }
    public void UnlockDoor()
    {
        unlock = true;
        if(lockImage != null)
        {
            lockImage.gameObject.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickToUnlock)
        {
            --clickCountToOpen;
            Debug.Log(clickCountToOpen);
            if (clickCountToOpen == 0)
            {
                UnlockDoor();
                doorSprite.color = Color.red;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && unlock)
        {
            GameManager.Instance.LevelCompleted();
        }
    }
}
