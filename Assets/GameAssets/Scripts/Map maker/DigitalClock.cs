using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hourHand;
    [SerializeField] TextMeshProUGUI minuteHand;
    [SerializeField] TextMeshProUGUI secondHand;
    [SerializeField] TextMeshProUGUI plusText;

    private int hour;
    private int minute;
    private int second;

    public int Hour { get { return hour; } set { hour = value; hourHand.text = hour.ToString("00"); } }
    public int Minute { get { return minute; } set { minute = value; minuteHand.text = minute.ToString("00"); } }
    public int Second { get { return second; } set { second = value; secondHand.text = second.ToString("00"); } }

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        //second++;
        //secondHand.text = second.ToString("00");
        Second = Second + 1;
        if (Second == 60)
        {
            Second = 0;
            //minute++;
            //minuteHand.text = minute.ToString("00");
            Minute = Minute + 1;
            if (Minute == 60)
            {
                Minute = 0;
                //hour++;
                //hourHand.text = hour.ToString("00");
                Hour = Hour + 1;
                if (Hour == 24)
                {
                    Hour = 0;
                }
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(StartTimer());
    }

    public void AddToSeconds()
    {
        Second = Second + 1;
        TextMeshProUGUI plusSecond = Instantiate(plusText, secondHand.transform.position, Quaternion.identity, secondHand.transform);
        Destroy(plusSecond.gameObject, 1.5f);
    }
    public void AddToMinutes()
    {
        Minute = Minute + 1;
        TextMeshProUGUI plusMinute = Instantiate(plusText, minuteHand.transform.position, Quaternion.identity, minuteHand.transform);
        Destroy(plusMinute.gameObject, 1.5f);
    }
    public void AddToHours()
    {
        Hour = Hour + 1;
        TextMeshProUGUI plusHour = Instantiate(plusText, hourHand.transform.position, Quaternion.identity, hourHand.transform);
        Destroy(plusHour.gameObject, 1.5f);
        if (Hour == 9)
        {
            GameEvents.UnlockDoor();
        }
    }
}
