using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Coefficients", menuName = "Stat Coefficients")]
public class StatCoefficients : ScriptableObject
{
    public float SPEED;
    public float HEALTH;
    public float ENERGY;
    public float DAMAGE;
    public float ISPOS;
    public float ATTACK_RANGE;
    public float SIGHT_RANGE;
    public float WALKPOINT_RANGE;
}
