using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactionRange = 5f;

    bool isFocused = false;
    Interactable target;
    public virtual void Interact()
    {
        Debug.Log("Interacting to:" + target.name);
    }
    void Update()
    {
        if (isFocused)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= interactionRange)
            {
                Interact();
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

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, interactionRange);
    }
}
