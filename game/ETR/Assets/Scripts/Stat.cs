using UnityEngine;
[System.Serializable]
public class Stat
{
    [SerializeField] float maxValue;
    [SerializeField] float value;

    public float GetMaxValue() => maxValue;
    public void SetMaxValue(float maxValue)
    {
        this.maxValue = maxValue;
    }

    public float GetValue() => value;
    public void SetValue(float value)
    {
        this.value = value;
    }

}
