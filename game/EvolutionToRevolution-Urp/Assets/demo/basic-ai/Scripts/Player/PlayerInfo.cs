using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private List<Transform> texts = new List<Transform>();
    private Transform selectedPlayer;
    private void Start()
    {
        foreach (Transform child in transform)
            texts.Add(child);
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    selectedPlayer = hit.transform;
                }
            }
        }

        if (selectedPlayer != null)
        {
            Print();
        }

    }

    private void Print()
    {
        Player player = selectedPlayer.GetComponent<Player>();
        texts[0].GetComponent<Text>().text = "Player Type: " + player.playerType.ToString();
        texts[1].GetComponent<Text>().text = "Name: " + player.playerName;
        texts[2].GetComponent<Text>().text = "Food Name: " + player.foodName;
        texts[3].GetComponent<Text>().text = "Max Health: " + player.maxHealth.ToString();
        texts[4].GetComponent<Text>().text = "Health: " + player.health.ToString();
        texts[5].GetComponent<Text>().text = "Max Energy: " + player.maxEnergy.ToString();
        texts[6].GetComponent<Text>().text = "Energy: " + player.energy.ToString();
        texts[7].GetComponent<Text>().text = "Speed: " + player.speed.ToString();
        texts[8].GetComponent<Text>().text = "Sight Range: " + player.sightRange.ToString();
        texts[9].GetComponent<Text>().text = "Attack Range: " + player.attackRange.ToString();
        texts[10].GetComponent<Text>().text = "Power: " + player.power.ToString();
        texts[11].GetComponent<Text>().text = "Age: " + player.age.ToString();
        texts[12].GetComponent<Text>().text = "Gen: " + player.gen.ToString();
        texts[13].GetComponent<Text>().text = "Child Count: " + player.childCount.ToString();
        texts[14].GetComponent<Text>().text = "Kill Count: " + player.killCount.ToString();
        texts[15].GetComponent<Text>().text = "Birth Day: " + player.birthDay.ToString();
        texts[16].GetComponent<Text>().text = "Bite Amount: " + player.biteAmount.ToString();
    }
}
