using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeInfo : MonoBehaviour
{
    private List<Transform> texts = new List<Transform>();
    void Start()
    {
        foreach (Transform child in transform)
            texts.Add(child);
    }

    void Update()
    {
        texts[0].GetComponent<Text>().text = "Day Time: " + TimeCycle.instance.dayTime.ToString();
        texts[1].GetComponent<Text>().text = "Day: " + TimeCycle.instance.day.ToString();
        texts[2].GetComponent<Text>().text = "Time Step: " + TimeCycle.instance.timeStep.ToString();
    }
}
