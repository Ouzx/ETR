using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInfo : MonoBehaviour
{
    private List<Transform> texts = new List<Transform>();
    private Transform selectedFood;
    void Start()
    {
        foreach (Transform child in transform)
            texts.Add(child);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Food")
                {
                    selectedFood = hit.transform;
                }
            }
        }
        if (selectedFood != null)
        {
            Print();
        }
    }
    private void Print()
    {
        Food food = selectedFood.GetComponent<Food>();
        texts[0].GetComponent<Text>().text = "Food Amount: " + food.foodAmount.ToString();
        texts[1].GetComponent<Text>().text = "Remaning Amount: " + food.remaningAmount.ToString();
        texts[2].GetComponent<Text>().text = "Regeneration Amount: " + food.regenerationAmount.ToString();
    }
}

