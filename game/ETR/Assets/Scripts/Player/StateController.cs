using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{  
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnStateChanged(State state)
    {
        Debug.Log(state);
        if (state == State.ChasingEnemy || state == State.ChasingFood || state == State.GoingBase || state == State.Patrolling || state == State.WaitingAtBase)
        {
            anim.ResetTrigger("attack");
            anim.ResetTrigger("eat");
            anim.SetTrigger("jog");
        }
        else if (state == State.Attacking)
        {
            anim.ResetTrigger("jog");
            anim.ResetTrigger("eat");
            anim.SetTrigger("attack");
        }
        else if (state == State.Eating)
        {
            anim.ResetTrigger("attack");
            anim.ResetTrigger("jog");
            anim.SetTrigger("eat");
        }
        else
        {
            anim.ResetTrigger("attack");
            anim.ResetTrigger("eat");
            anim.ResetTrigger("jog");
            anim.SetTrigger("die");
        }
    }
}
public enum State { WaitingAtBase, GoingBase, Patrolling, ChasingEnemy, ChasingFood, Attacking, Eating, Idle, Dead }

