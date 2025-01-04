using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : GenericPoolableObject
{
    private void OnEnable()
    {
        StartCoroutine(ReturnObjToPool(1));
    }
    public IEnumerator ReturnObjToPool(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ReturnToPool();
    } 
}
