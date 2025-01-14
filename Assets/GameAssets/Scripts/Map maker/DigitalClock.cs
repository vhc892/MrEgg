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
        while (true)
        {
            // Increment seconds
            Second++;
            if (Second >= 60)
            {
                Second = 0;
                Minute++;

                // Increment minutes
                if (Minute >= 60)
                {
                    Minute = 0;
                    Hour++;

                    // Increment hours
                    if (Hour >= 24)
                    {
                        Hour = 0;
                    }
                }
            }

            // Wait for 1 second before repeating
            yield return new WaitForSeconds(1);
        }
    }

    public void AddToSeconds()
    {
        Second = Second + 1;
        TextMeshProUGUI plusSecond = Instantiate(plusText,transform);
        plusSecond.transform.SetParent(secondHand.transform);
        plusSecond.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (Second >= 60)
        {
            Second = 0;
            Minute = Minute + 1;
            if (Minute >= 60)
            {
                Minute = 0;
                Hour = Hour + 1;
                if (Hour >= 24)
                {
                    Hour = 0;
                }
            }
        }
        Destroy(plusSecond.gameObject, 1.5f);
    }
    public void AddToMinutes()
    {
        Minute = Minute + 1;
        TextMeshProUGUI plusMinute = Instantiate(plusText, transform);
        plusMinute.transform.SetParent(minuteHand.transform);
        plusMinute.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (Minute >= 60)
        {
            Minute = 0;
            Hour = Hour + 1;
            if (Hour >= 24)
            {
                Hour = 0;
            }
        }
        Destroy(plusMinute.gameObject, 1.5f);
    }
    public void AddToHours()
    {
        Hour = Hour + 1;
        TextMeshProUGUI plusHour = Instantiate(plusText, transform);
        plusHour.transform.SetParent(hourHand.transform);
        plusHour.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (Hour >= 24)
        {
            Hour = 0;
        }
        if (Hour >= 9)
        {
            GameEvents.UnlockDoor();
        }
        Destroy(plusHour.gameObject, 1.5f);
    }
}
