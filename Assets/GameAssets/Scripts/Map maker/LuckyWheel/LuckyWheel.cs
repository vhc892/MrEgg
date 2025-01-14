using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyWheel : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    [SerializeField] private float spinSpeed;
    [SerializeField] Key keyPf;
    [SerializeField] WheelPointer wheelPointer;
    [SerializeField] GameObject[] randomStuffs;

    public delegate void OnSpinWheel();
    public static event OnSpinWheel onSpinWheel;
    public static void SpinWheel()
    {
        onSpinWheel?.Invoke();
    }

    public delegate void OnStopWheel();
    public static event OnStopWheel onStopWheel;
    public static void StopWheel()
    {
        onStopWheel?.Invoke();
    }
    private void OnReward()
    {
        Debug.Log("Reward key");
        if (wheelPointer.getKey)
        {
            Key key = Instantiate(keyPf, transform.position, Quaternion.identity);
            key.door = FindObjectOfType<FinishDoor>();
            key.transform.SetParent(transform);
            key.transform.DOLocalPath(new Vector3[] { key.transform.position, Vector3.up * 4, Vector3.right * 4 }, 1f);
        }
        else
        {
            int rand = Random.Range(0, randomStuffs.Length-1);
            GameObject randomStuff = Instantiate(randomStuffs[rand], transform);
            randomStuff.transform.localPosition = Vector2.zero;
        }
    }

    public void RotateWheel()
    {
        //yield return new WaitForSeconds(1);
        spinSpeed = Random.Range(-36f, 36f) + 360 * 5;
        Vector3 spinAngle = new Vector3(0, 0, spinSpeed);
        wheel.transform.DORotate(spinAngle, 1f, RotateMode.FastBeyond360).OnComplete(delegate
        {
            Debug.Log("Finish spin");
            StopWheel();
        });
    }

    private void OnEnable()
    {
        onSpinWheel += RotateWheel;
        onStopWheel += OnReward;
    }

    private void OnDisable()
    {
        onSpinWheel -= RotateWheel;
        onStopWheel -= OnReward;
    }
}
