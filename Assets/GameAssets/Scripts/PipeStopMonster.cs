using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PipeStopMonster : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MonsterTakeKey monster;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("hold");
        monster.isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("not hold");
        monster.isHolding = false;
        monster.canMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            monster.canMove = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            monster.canMove = true;
        }
    }
}
