using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueBalloon : MonoBehaviour
{
    LineRenderer line;
    [HideInInspector] public bool isRescueing = false;
    [HideInInspector] public GameObject rescueTarget;
    [HideInInspector] public Vector2 startPos;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (isRescueing)
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, rescueTarget.transform.position + Vector3.up);
        }
        else
        {
            line.enabled = false;
        }
    }

    private void StopRescue()
    {
        isRescueing = false;
    }

    private void OnEnable()
    {
        GameEvents.onLevelRestart += StopRescue;
    }

    private void OnDisable()
    {
        GameEvents.onLevelRestart -= StopRescue;
    }
}
