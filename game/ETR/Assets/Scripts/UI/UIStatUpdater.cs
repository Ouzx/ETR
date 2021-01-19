using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatUpdater : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    GameManager gm;
    void Start()
    {
        gm = GameManager.instance;
        gm.OnUIUpdate += UpdateTexts;    
    }
    void UpdateTexts(int evolutionPoint, int revolutionPoint)
    {
        texts[0].text = gm.uiPlayer.health.ToString();
        texts[1].text = gm.uiPlayer.energy.ToString();
        texts[2].text = gm.uiPlayer.speed.ToString();
        texts[3].text = gm.uiPlayer.ispos.ToString();
        texts[4].text = gm.uiPlayer.damage.ToString();
        texts[5].text = gm.uiPlayer.sightRange.ToString();
        texts[6].text = gm.uiPlayer.healthRegen.ToString();
        texts[7].text = gm.uiPlayer.energyRegen.ToString();
        texts[8].text = gm.uiPlayer.power.ToString();
        texts[9].text = evolutionPoint.ToString();
        texts[10].text = revolutionPoint.ToString();
    }
}
