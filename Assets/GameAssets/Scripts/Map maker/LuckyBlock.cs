using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Levels
{
    level11,
    level13,
    level14,
}
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


    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        original.SetActive(true);
        reveal.SetActive(false);
        broken.SetActive(false);

        FunctionOnInit();
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
        }
    }

    private void FunctionOnHit()
    {
        switch (levelIndex)
        {
            case Levels.level11:
                Level11onHit();
                break;
            case Levels.level13:
                Level13onHit();
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
    void OnHit()
    {
        if (isBroken)
        {
            return;
        }
        switch (numberOfHits)
        {
            case 1:
                reveal.SetActive(true);
                original.SetActive(false);
                break;
            case 0:
                broken.SetActive(true);
                reveal.SetActive(false);
                original.SetActive(false);
                isBroken = true;
                break;
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
            numberOfHits--;
            OnHit();
            FunctionOnHit();
        }
    }

}