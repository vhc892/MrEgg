using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using DG.Tweening;
using Hapiga.UI;

public class LuckyBlock : MonoBehaviour
{
    [SerializeField] GameObject original;
    [SerializeField] GameObject reveal;
    [SerializeField] GameObject broken;
    [SerializeField] int numberOfHits = 1;
   
    bool isBroken = false;

    public Levels levelIndex;
    [Header("OBJECTS ACCORDING TO LEVEL")]

    //LEVEL11
    [ShowIf("levelIndex", Levels.level11)]
    public Collider2D invisPath;

    //LEVEL13
    [ShowIf("levelIndex", Levels.level13)]
    [SerializeField] GameObject[] timeAdjust;
    [ShowIf("levelIndex", Levels.level13)]
    [SerializeField] DigitalClock digitalClock;

    //LEVEL14
    [ShowIf("levelIndex", Levels.level14)]
    [SerializeField] GameObject flower;
    [ShowIf("levelIndex", Levels.level14)]
    [SerializeField] GameObject tree;
    [ShowIf("levelIndex", Levels.level14)]
    [SerializeField] GameObject fertilizer;
    [ShowIf("levelIndex", Levels.level14)]
    [SerializeField] GameObject seed;
    [ShowIf("levelIndex", Levels.level14)]
    [SerializeField] GameObject soil;

    //LEVEL22
    [ShowIf("levelIndex", Levels.level22)]
    [SerializeField] int passcode;
    [ShowIf("levelIndex", Levels.level22)]
    [SerializeField] GameObject starPrefab;

    //LEVEL27
    [ShowIf("levelIndex", Levels.level27)]
    [SerializeField] UIPanel endText;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        FunctionOnInit();
        SetBlockState();
    }

    private void FunctionOnInit()
    {
        switch (levelIndex)
        {
            case Levels.level11:
                Level11onInit();
                break;
            case Levels.level13:
                Level13onInit();
                break;
            case Levels.level14:
                Level14onInit();
                break;
            case Levels.level22:
                Level22onInit();
                break;
            case Levels.level27:
                Level27onInit();
                break;
        }
    }

    private void FunctionOnHit()
    {
        if (isBroken) return;
        switch (levelIndex)
        {
            case Levels.level11:
                Level11onHit();
                break;
            case Levels.level13:
                Level13onHit();
                break;
            case Levels.level14:
                Level14onHit();
                break;
            case Levels.level22:
                Level22onHit();
                break;
            case Levels.level27:
                Level27onHit();
                break; 
        }
    }
    #region LEVEL11
    private void Level11onInit()
    {
        invisPath.enabled = false;
    }
    private void Level11onHit()
    {
        invisPath.enabled = true;
    }
    #endregion

    #region LEVEL13
    private void Level13onInit()
    {

    }
    private void Level13onHit()
    {
        int nearestPoint = CheckNearestPoint();
        switch (nearestPoint)
        {
            case 0:
                digitalClock.AddToHours();
                break;
            case 1:
                digitalClock.AddToMinutes();
                break;
            case 2:
                digitalClock.AddToSeconds();
                break;
        }
    }

    private int CheckNearestPoint()
    {
        int nearestPoint = 0;
        float shortestDistance = Vector2.Distance(timeAdjust[0].transform.position, transform.position);
        for (int i = 0; i < timeAdjust.Length; i++)
        {
            float newDistance = Vector2.Distance(timeAdjust[i].transform.position, transform.position);
            if (newDistance < shortestDistance)
            {
                shortestDistance = newDistance;
                nearestPoint = i;
            }
        }
        return nearestPoint;
    }
    #endregion

    #region LEVEL14

    private void Level14onInit()
    {

    }

    private void Level14onHit()
    {
        Flower flowerScript = flower.GetComponent<Flower>();
        switch (numberOfHits)
        {
            case 1:
                seed.transform.DOMove(seed.transform.position + Vector3.up * 2, 1f).OnComplete(() =>
                {
                    seed.transform.DOMove(flower.transform.position, 1f).OnComplete(() =>
                    {
                        seed.SetActive(false);
                        flower.SetActive(true);
                        flower.transform.DOMove(flower.transform.position + Vector3.up * 2, 1f).OnComplete(() =>
                        {
                            flowerScript.flowerCollider.enabled = true;
                        });
                    });
                });
                break;
            case 0:
                fertilizer.transform.DOMove(fertilizer.transform.position + Vector3.up * 2, 1f).OnComplete(() =>
                {
                    fertilizer.transform.DOMove(tree.transform.position, 1f).OnComplete(() =>
                    {
                        fertilizer.SetActive(false);
                        if (!flowerScript.isPlucked)
                        {
                            flower.SetActive(false);
                            tree.SetActive(true);
                            tree.transform.DOMove(tree.transform.position + Vector3.up * 4, 1f).OnComplete(() => tree.GetComponent<Tree>().StartDrag());
                        }
                        else
                        {
                            soil.GetComponent<SpriteRenderer>().color = Color.grey;
                        }
                    });
                });
                break;
        }
    }

    #endregion

    #region LEVEL22
    private void Level22onInit()
    {
        numberOfHits = passcode;
    }

    private void Level22onHit()
    {
        if (numberOfHits <= 0)
        {
            return;
        }
        ObjPool star = Pool.Instance.star.GetPrefabInstance();
        star.transform.SetParent(transform);
        star.transform.localPosition = Vector3.zero;
        //star.ReturnObjToPool(1f);
    }
    #endregion

    #region LEVEL27
    private void Level27onInit()
    {

    }

    private void Level27onHit()
    {
        endText.Show(true);
    }
    #endregion
    void OnHit()
    {
        FunctionOnHit();
        numberOfHits--;
        SetBlockState();
    }

    void SetBlockState()
    {
        if (numberOfHits >= 1)
        {
            isBroken = false;
            original.SetActive(true);
            reveal.SetActive(false);
            broken.SetActive(false);
        }
        else if (numberOfHits == 0)
        {
            isBroken = false;
            original.SetActive(false);
            reveal.SetActive(true);
            broken.SetActive(false);
        }
        else
        {
            original.SetActive(false);
            reveal.SetActive(false);
            broken.SetActive(true);
            isBroken = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.y > transform.position.y)
            {
                return;
            }
            OnHit();

        }
    }

}