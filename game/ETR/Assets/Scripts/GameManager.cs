using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        SelectPlayers();
        
    }

    #endregion

    public UIPlayer uiPlayer;
    public Transform PlayerContainer;
    Transform players;
    public Action<int,int> OnUIUpdate;

    [SerializeField]Sprite[] pps;
    [SerializeField]Image pp;
    [SerializeField]int EvolutionPoint = 10;
    [SerializeField]int RevolutionPoint = 10;

    void Start()
    {
        GetAllStatsAverage();
    }

    #region EP RP
    public void EarnEP(int amount) => EvolutionPoint += amount;
    public bool SpendEP(int amount)
    {
        if (EvolutionPoint - amount >= 0)
        {
            EvolutionPoint -= amount;
            return true;
        }
        return false;
    }

    public void EarnRP(int amount) => RevolutionPoint += amount;
    public bool SpendRP(int amount)
    {
        if (RevolutionPoint - amount >= 0)
        {
            RevolutionPoint -= amount;
            return true;
        }
        return false;
    }
    #endregion

    #region UIPlayer
    void SelectPlayers()
    {
        switch (uiPlayer.interactableTypes)
        {
            case InteractableTypes.Fred:
                players = PlayerContainer.GetChild(0);
                pp.sprite = pps[0];
                break;

            case InteractableTypes.Barney:
                players = PlayerContainer.GetChild(1);
                pp.sprite = pps[1];
                break;

            case InteractableTypes.Ivedo:
                players = PlayerContainer.GetChild(2);
                pp.sprite = pps[2];
                break;
        }
    }
    
    void GetAllStatsAverage()
    {
        uiPlayer.Reset();
        int counter = 0;
        foreach (Transform _player in players)
        {
            counter++;
            Player tempPlayer = _player.GetComponent<Player>();
            uiPlayer.health += tempPlayer.health.GetMaxValue();
            uiPlayer.energy += tempPlayer.energy.GetMaxValue();
            uiPlayer.speed += tempPlayer.speed.GetMaxValue();
            uiPlayer.ispos += tempPlayer.ispos.GetMaxValue();
            uiPlayer.damage += tempPlayer.damage.GetMaxValue();
            uiPlayer.sightRange += tempPlayer.sightRange.GetMaxValue();
            uiPlayer.healthRegen += tempPlayer.healthRegen;
            uiPlayer.energyRegen += tempPlayer.energyRegen;
            uiPlayer.power += tempPlayer.power;
        }
        uiPlayer.health /= counter;
        uiPlayer.energy /= counter;
        uiPlayer.speed /= counter;
        uiPlayer.ispos /= counter;
        uiPlayer.damage /= counter;
        uiPlayer.sightRange /= counter;
        uiPlayer.healthRegen /= counter;
        uiPlayer.energyRegen /= counter;
        uiPlayer.power /= counter;
        OnUIUpdate?.Invoke(EvolutionPoint, RevolutionPoint);

    }

    #region Setters
    public void Setter(string statType, float value)
    {
        foreach (Transform _player in players)
        {
            Player tempPlayer = _player.GetComponent<Player>();
            switch (statType)
            {
                case "health":
                    SetStats(tempPlayer.health, value);
                    break;
                case "energy":
                    SetStats(tempPlayer.energy, value);
                    break;
                case "speed":
                    SetStats(tempPlayer.speed, value);
                    break;
                case "ispos":
                    SetStats(tempPlayer.ispos, value);
                    break;
                case "damage":
                    SetStats(tempPlayer.damage, value);
                    break;
                case "sightRange":
                    SetStats(tempPlayer.sightRange, value);
                    break;
                case "healthRegen":
                    tempPlayer.healthRegen += value;
                    break;
                case "energyRegen":
                    tempPlayer.energyRegen += value;
                    break;
            }
        }
        GetAllStatsAverage();
    }
    void SetStats(Stat stat,float value)
    {
        stat.AppendMaxValue(value);
    }
    #endregion
    #endregion
}
