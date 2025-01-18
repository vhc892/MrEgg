using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterQuote : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject monster;
    private CharController player;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    public void MoveQuote()
    {
        Debug.Log("MoveQuote started!");

        Vector3 targetPosition = player.transform.position + new Vector3(1f, 3f, 0f);

        // quote to player
        transform.DOMove(targetPosition, 1f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                // circle to monster 
                Vector3 startPosition = circle.transform.position;
                Vector3 endPosition = monster.transform.position;

                Vector3 controlPoint = (startPosition + endPosition) / 2 + Vector3.up * 3f;
                Vector3[] path = new Vector3[] { startPosition, controlPoint, endPosition };

                Sequence sequence = DOTween.Sequence();
                sequence.Append(circle.transform.DOPath(path, 1f, PathType.CatmullRom).SetEase(Ease.InOutQuad));

                // scale up
                sequence.Insert(0, circle.transform.DOScale(2f, 0.5f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        circle.transform.DOScale(1f, 0.5f).SetEase(Ease.InQuad);
                    }));

                sequence.OnComplete(() =>
                {
                    Debug.Log("MoveQuote completed!");
                    monster.SetActive(false);
                    circle.SetActive(false);
                    this.gameObject.SetActive(false);
                });
            });
    }
}
