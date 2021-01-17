using UnityEngine;
[System.Serializable]
public class Stat
{
    [HideInInspector] public Player temp;
    [SerializeField] float maxValue;
    float value;

    public float GetMaxValue() => maxValue;
    public void SetMaxValue(float maxValue, bool alreadySet = false)
    {
        this.maxValue = maxValue;
        if (!alreadySet) temp.OnStatChanged();

    }

    public float GetValue() => value;
    public void SetValue(float value)
    {
        this.value = value;
        // if (!alreadySet) temp.OnStatChanged();

    }
}
