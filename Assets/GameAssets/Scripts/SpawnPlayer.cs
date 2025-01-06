using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPlayer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject playerClone;
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(playerClone, transform.position, Quaternion.identity);
    }

}
