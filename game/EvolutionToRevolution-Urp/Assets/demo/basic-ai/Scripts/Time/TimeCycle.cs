using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    #region Singleton
    public static TimeCycle instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    #region TimeParameters
    public float dayTime = 0;
    public int day = 0;
    [Range(0, 24)] public float timeStep;
    #endregion
    #region Events

    #endregion
    public event Action OnSecondChanged;
    public event Action OnDawn;
    public event Action OnSunSet;
    private void Start()
    {
        InvokeRepeating(nameof(Cycle), 1, 1);
    }

    private void Cycle()
    {
        OnSecondChanged?.Invoke();
    }

    void FixedUpdate()
    {
        dayTime += timeStep;
        if (dayTime > 24)
        {
            dayTime = 0;
            day++;
        }
        else if (dayTime == 6) OnDawn?.Invoke();
        else if (dayTime == 18) OnSunSet?.Invoke();
    }
}
