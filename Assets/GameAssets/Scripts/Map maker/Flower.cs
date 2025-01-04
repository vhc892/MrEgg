using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flower : MonoBehaviour
{
    public Collider2D flowerCollider;
    public bool isPlucked = false;

    private void OnMouseDown()
    {
        Debug.Log("Flower clicked");
        isPlucked = true;
        GetComponent<Animator>().Play("pluck flower");
    }
}
