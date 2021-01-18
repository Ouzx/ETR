using System;
using System.Collections;
using UnityEngine;

[ExecuteAlways]
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
    #endregion

    bool[] timeKeys = { true, true };
    void FixedUpdate()
    {
        tempTime += timeStep;
        dayTime = Mathf.Round(tempTime);
        if (dayTime == 6 && timeKeys[0]) { OnMorning?.Invoke(); timeKeys[0] = false; }
        else if (dayTime == 18 && timeKeys[1]) { OnEvening?.Invoke(); timeKeys[1] = false; }
        else if (dayTime > 24)
        {
            timeKeys[0] = true; timeKeys[1] = true;
            tempTime = 0;
            day++;
        }
    }
}
