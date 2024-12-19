using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishDoor : MonoBehaviour
{
    [SerializeField] private bool unlock;
    [SerializeField] private SpriteRenderer lockImage;

    public void UnlockDoor()
    {
        unlock = true;
        lockImage.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && unlock)
        {
            GameManager.Instance.LevelCompleted();
        }
    }
}
