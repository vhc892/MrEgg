using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPageRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPageRect.localPosition;
    }

    public void Next()
    {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
            Debug.Log("currentPage" + currentPage);
            Debug.Log(targetPos.x);
        }
    }
    public void Previus()
    {
        if(currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
            Debug.Log("currentPage" + currentPage);
        }
    }
    private void MovePage()
    {
        levelPageRect.localPosition = targetPos;
        //levelPageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        //levelPageRect.DOLocalMove(targetPos, tweenTime);
        Debug.Log("move effect");
    }
}
