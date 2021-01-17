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
    Player player;
    bool isFocused = false;
    Interactable target;
    void Awake()
    {
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
        {
            player = GetComponent<PlayerController>().player;
            interactableType = player.InteractableType;
        }
        else interactableType = InteractableTypes.Food;
    }
    void Update()
    {
        // Triggering INTERACTIONS can be INVOKE
        if (isFocused && target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= player.attackRange.GetMaxValue())
            {
                if (target.interactableType == InteractableTypes.Food)
                {
                    Eat();
                }
                else if (target.interactableType != player.InteractableType)
                {
                    // Attack()
                }
            }
        }
    }
    #region Interaction
    void Eat()
    {
        player.Eat(target.GetComponent<Food>().Eat(player.damage.GetValue()));
    }
    void Attack()
    {
        // player.Attack(); -> target.GetDamage();
    }
    #endregion

    #region Focus
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
    #endregion
}
