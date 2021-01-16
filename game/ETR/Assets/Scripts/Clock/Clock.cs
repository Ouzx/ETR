using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    #region Singleton
    public static Clock instance;
    void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    #region ClockParameters
    public float dayTime = 0;
    public int day = 0;
    [Range(0, 24)] public float timeStep;
    #endregion

    #region Events
    public event Action OnMorning;
    public event Action OnEvening;
    #endregion

    void FixedUpdate()
    {
        dayTime += timeStep;
        if (dayTime == 6) OnMorning?.Invoke();
        else if (dayTime == 18) OnEvening?.Invoke();
        else if (dayTime > 24)
        {
            dayTime = 0;
            day++;
        }
    }
}
