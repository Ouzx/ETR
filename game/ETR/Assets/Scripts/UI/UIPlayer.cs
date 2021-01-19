using System.Reflection;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    public void Reset()
    {
        FieldInfo[] fields = typeof(UIPlayer).GetFields();
        foreach (FieldInfo _field in fields)
        {
            if (_field.FieldType == typeof(float))
            {
                _field.SetValue(this, 0);
            }
        }
    }
    public InteractableTypes interactableTypes;
    public float health;
    public float energy;
    public float speed;
    public float ispos;
    public float damage;
    public float sightRange;
    public float healthRegen;
    public float energyRegen;
    public float power;

}
