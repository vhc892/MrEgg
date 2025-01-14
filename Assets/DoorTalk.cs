using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorTalk : MonoBehaviour, IPointerDownHandler
{
    public GameObject talkBubble;
    private Vector3 previousScale;
    private Vector3 originalPosition;

    private void Start()
    {
        previousScale = transform.localScale;
        originalPosition = talkBubble.transform.localPosition;
    }

    private void Update()
    {
        if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(previousScale.x))
        {
            Vector3 bubbleScale = talkBubble.transform.localScale;
            talkBubble.transform.localScale = new Vector3(-bubbleScale.x, bubbleScale.y, bubbleScale.z);

            if (transform.localScale.x < 0)
            {
                talkBubble.transform.localPosition = new Vector3(originalPosition.x + 6.5f, originalPosition.y, originalPosition.z);
            }
            else
            {
                talkBubble.transform.localPosition = originalPosition;
            }
            previousScale = transform.localScale;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        talkBubble.SetActive(true);
        StartCoroutine(TurnOffTalkBubble());
    }

    private IEnumerator TurnOffTalkBubble()
    {
        yield return new WaitForSeconds(2f);
        talkBubble.SetActive(false);
    }
}
