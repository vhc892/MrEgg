using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChildCtrl : MonoBehaviour
{
    [HideInInspector] public Collider2D col;
    CharController charController;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        charController = GetComponentInParent<CharController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border" && GameConfig.Instance.CurrentLevel != 23)
        {
            charController.ReturnToStartPosition();
        }
        else if (collision.gameObject.tag == "Border" && GameConfig.Instance.CurrentLevel == 23)
        {
            Debug.Log("Move camera");
            StartCoroutine(MoveCameraToPosition(new Vector3(Camera.main.transform.position.x, -9, Camera.main.transform.position.z), 1f));
        }
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = Camera.main.transform.position;
        float speed = Vector3.Distance(startPosition, targetPosition) / duration;

        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.01f)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targetPosition, 40 * Time.deltaTime);
            yield return null;
        }

        Camera.main.transform.position = targetPosition;
    }

}
