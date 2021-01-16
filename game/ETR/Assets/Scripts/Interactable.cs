using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableTypes { Fred, Barney, Ivedo, Food }
public class Interactable : MonoBehaviour
{
    /*   INTERACTION
     * - When player find an interactble object, it triggers to it's own interactable class. 
     * - And the player's interactable class triggers to object's interactable classs.
     * - If the object is enemy, player triggers "Attack" method and attack method triggers to "GetAttacked" methods from object.
     * - If the object is food, player triggers "Eat" method and eat method triggers to "GetBitten" method from object. 
     */

    [HideInInspector] public InteractableTypes interactableType;
    Stats stats;
    bool isFocused = false;
    Interactable target;
    void Awake()
    {
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
        {
            stats = GetComponent<PlayerController>().stats;
            interactableType = stats.InteractableType;
        }
        else interactableType = InteractableTypes.Food;
    }
    void Update()
    {
        if (isFocused && target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= stats.attackRange.GetMaxValue())
            {
                // Triggering INTERACTIONS can be INVOKE
                if (target.interactableType == InteractableTypes.Food)
                {
                    //Eat
                    Debug.Log(target.GetComponent<Food>().Eat(5));
                }
                else if (target.interactableType != stats.InteractableType)
                {
                    // Engage 
                }
            }
        }
    }
    public void OnFocused(Interactable newTarget)
    {
        isFocused = true;
        target = newTarget;
    }
    public void OnDefocused()
    {
        isFocused = false;
        target = null;
    }
}
