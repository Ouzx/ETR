using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    GameManager gm;
    public Button increaser, decreaser;
    public string statType;
    public float Increasement = .5f;
    int level = 0;
    int cost = 1;
    void Start()
    {
        gm = GameManager.instance;
        increaser.onClick.AddListener(IIncrease);
        decreaser.onClick.AddListener(IDecrease);
    }
    void IIncrease() => Invoke(nameof(Increase), .1f);
    void IDecrease() => Invoke(nameof(Decrease), .1f);

    void Increase()
    {
        level++;
        if (gm.SpendEP(cost))
        {
            gm.Setter(statType, Increasement);
            IncreaseCost();
        }
    }
    void Decrease()
    {
        level--;
        if (gm.SpendEP(cost))
        {
            gm.Setter(statType, -Increasement);
            IncreaseCost();
        }
    }
    void IncreaseCost()
    {
        cost = Convert.ToInt32(Mathf.Pow(1.2f, level));
    }
}
