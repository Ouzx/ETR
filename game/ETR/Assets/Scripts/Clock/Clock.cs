using System;
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
    #endregion

    #region ClockParameters
    [Range(0, 24)]
    public float dayTime = 0;
    public int day = 0;
    [Range(0, 1f)]
    public float timeStep;
    #endregion


    void FixedUpdate()
    {
        float pointFloater = 100f;
        dayTime = Mathf.Round((dayTime + timeStep) * pointFloater) / pointFloater;
        if (dayTime == 6) OnMorning?.Invoke();
        else if (dayTime == 18) OnEvening?.Invoke();
        else if (dayTime > 24)
        {
            dayTime = 0;
            day++;
        }
    }
}
