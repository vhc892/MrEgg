using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] GameObject ground;
    private DragObject dragObject;
    private bool dragable = false;
    private float distanceToTree;
    public void StartDrag()
    {
        dragable = true;
        dragObject = gameObject.GetComponent<DragObject>();
        dragObject.allowDragY = true;
        distanceToTree = Vector3.Distance(ground.transform.position, transform.position);
    }

    private void Update()
    {
        if (dragable)
        { 
            float xAxis = ground.transform.position.x;
            float yAxis = transform.position.y - distanceToTree;
            ground.transform.position = new Vector3(xAxis, yAxis,0);
        }
    }
}
