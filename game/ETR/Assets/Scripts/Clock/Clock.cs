using System;
using System.Collections;
using UnityEngine;
using TMPro;
public class Clock : MonoBehaviour
{
    #region Singleton
    public static Clock instance;
    void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    #region Events
    public event Action OnMorning;
    public event Action OnEvening;
    public event Action OnSecond;
    void Start()
    {
        tempTime = dayTime;
        StartCoroutine(nameof(SecondCounter));
    }
    IEnumerator SecondCounter()
    {
        while (true)
        {
            if (OnSecond != null)
                OnSecond?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

    #region ClockParameters
    public int day = 0;
    float tempTime;
    [Range(0, 24)] public float dayTime;

    [Range(0, 1f)] public float timeStep;
    [SerializeField] TextMeshProUGUI clock;
    #endregion

    bool[] timeKeys = { true, true };
    void FixedUpdate()
    {
        tempTime += timeStep;

        SetClock();
        if (dayTime == 6 && timeKeys[0]) { if (OnMorning != null) OnMorning?.Invoke(); timeKeys[0] = false; }
        else if (dayTime == 18 && timeKeys[1]) { if (OnEvening != null) OnEvening?.Invoke(); timeKeys[1] = false; }
        else if (dayTime > 24)
        {
            timeKeys[0] = true; timeKeys[1] = true;
            tempTime = 0;
            day++;
        }
    }

    void SetClock()
    {
        dayTime = Mathf.Round(tempTime);
        int clockTimeLeft = Mathf.FloorToInt(tempTime);
        string clockTimeRight = Mathf.Round(ScaleTime(tempTime - clockTimeLeft) * 100).ToString("00");
        clock.text = clockTimeLeft.ToString("00") + ":" + clockTimeRight;
    }
    float ScaleTime(float OldValue)
    {
        float NewValue = (OldValue * 60 / 100);

        return (NewValue);
    }
}
