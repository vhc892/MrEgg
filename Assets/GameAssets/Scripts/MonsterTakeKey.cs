using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakeKey : MonoBehaviour
{
    [SerializeField] private Key key;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    private CharController player;
    private float detectionRadius = 2f;
    public bool stop;


    private void Start()
    {
        player = GameManager.Instance.player;
    }
    private void Update()
    {
        float sqrDistance = (transform.position - player.transform.position).sqrMagnitude;
        if (sqrDistance <= detectionRadius * detectionRadius)
        {
            Debug.Log("monster run");
        }
    }
}
