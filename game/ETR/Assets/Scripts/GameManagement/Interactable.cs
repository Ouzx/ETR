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
    PlayerController pc;
    public int epOnEat, epOnHit;
    void Awake()
    {
        pc = GetComponent<PlayerController>();
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
                    pc.state = State.Eating;
                    StateController.instance.OnStateChanged(State.Eating);
                    Eat();
                }
                else
                {
                    pc.state = State.Attacking;
                    StateController.instance.OnStateChanged(State.Attacking);
                    Attack();
                }
            }
        }
    }
    #region Interaction
    bool alreadyInteracted;
    void ResetInteraction()
    {
        alreadyInteracted = false;
    }
    void Eat()
    {
        if (!alreadyInteracted)
        {
            player.Eat(target.GetComponent<Food>().Eat(player.damage.GetValue()));
            GameManager.instance.EarnEP(epOnEat, player.InteractableType);
            alreadyInteracted = true;
            Invoke(nameof(ResetInteraction), 1 / player.ispos.GetValue());
        }
    }

    void Attack()
    {
        if (!alreadyInteracted)
        {
            player.Attack();
            GameManager.instance.EarnEP(epOnHit, player.InteractableType);
            target.player.GetDamage(-player.damage.GetValue());
            alreadyInteracted = true;
            Invoke(nameof(ResetInteraction), player.ispos.GetValue());
        }
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
