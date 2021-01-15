using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float foodAmount, remainingFood, regenerationAmount;
    float sizeCoefficient = .005f;
    void Awake()
    {
        remainingFood = foodAmount;
    }
    void Start()
    {
        InvokeRepeating(nameof(UpdateScale), 0.5f, 0.05f);
    }

    void UpdateScale()
    {
        float scale = Mathf.Sqrt(remainingFood * sizeCoefficient);
        transform.localScale = Vector3.one * scale;
    }

    public float Eat(float bite)
    {
        float tempAmount = remainingFood - bite;
        if (tempAmount > 0)
        {
            remainingFood = tempAmount;
            return bite;
        }
        else
        {
            tempAmount = remainingFood;
            remainingFood = 0;
            // Invoke(nameof(DestroyFood), 0.1f);
            DestroyFood();
            return tempAmount;
        }
    }
    void DestroyFood()
    {
        Destroy(gameObject);
    }
}
