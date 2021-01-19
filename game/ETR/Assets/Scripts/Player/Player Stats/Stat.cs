using UnityEngine;
[System.Serializable]
public class Stat
{
     public Player temp;
    [SerializeField] float maxValue;
    float value;

    public void SetTemp(Player player) => temp = player;

    public float GetMaxValue() => maxValue;
    public void SetMaxValue(float maxValue, bool alreadySet = false)
    {
        this.maxValue = maxValue;
        if (!alreadySet) temp.OnStatChanged();
    }
    public void AppendMaxValue(float addition)
    {
        maxValue += addition;
        temp.OnStatChanged();
    }
    public void Equalize() => value = maxValue;

    public float GetValue() => value;
    public void SetValue(float value)
    {
        this.value = value;
        // if (!alreadySet) temp.OnStatChanged();

    }
    public void AppendValue(float addition)
    {
        value += addition;
        // if (!alreadySet) temp.OnStatChanged();

    }

}
