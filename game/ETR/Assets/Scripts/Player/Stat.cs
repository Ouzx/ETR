using UnityEngine;
[System.Serializable]
public class Stat
{
    [HideInInspector] public Stats temp;
    [SerializeField] float maxValue;
    [SerializeField] float value;

    public float GetMaxValue() => maxValue;
    public void SetMaxValue(float maxValue)
    {
        this.maxValue = maxValue;
        temp.OnStatChanged();

    }

    public float GetValue() => value;
    public void SetValue(float value)
    {
        this.value = value;
        temp.OnStatChanged();
    }

}
