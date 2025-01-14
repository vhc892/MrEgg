using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueBalloon : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] public Vector2 startPos;
    [HideInInspector] public bool isRescueing = false;
    [HideInInspector] public GameObject rescueTarget;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
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
        transform.position = startPos;
        isRescueing = false;
    }

    private void OnEnable()
    {
        GameEvents.onLevelStart += StopRescue;
        GameEvents.onLevelRestart += StopRescue;
    }

    private void OnDisable()
    {
        GameEvents.onLevelStart -= StopRescue;
        GameEvents.onLevelRestart -= StopRescue;
    }
}
