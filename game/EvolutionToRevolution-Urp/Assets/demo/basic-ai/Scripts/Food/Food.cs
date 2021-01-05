using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public float foodAmount;
    public float remaningAmount;
    public float regenerationAmount;
    private float sizeCoefficient = .005f;
    private void Start()
    {
        remaningAmount = foodAmount;
    }
    private void Update()
    {
        float scale = Mathf.Sqrt(remaningAmount * sizeCoefficient);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public float Eat(float bite)
    {
        float tempAmount = remaningAmount - bite;
        if (tempAmount < 0)
        {
            tempAmount = remaningAmount;
            remaningAmount = 0;
            Invoke(nameof(DestroyFood), 0.5f);
            return tempAmount;
        }
        else
        {
            remaningAmount = tempAmount;
            return bite;
        }
    }

    public void DestroyFood()
    {
        Destroy(gameObject);
    }

}
