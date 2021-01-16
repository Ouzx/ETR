using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Coefficients", menuName = "Coefficients/Stats")]
public class StatCoefficients : ScriptableObject
{
    public float Speed;
    public float Health;
    public float Energy;
    public float Damage;
    public float Ispos;
    public float AttackRange;
    public float SightRange;
    public float WalkPointRange;
    public float SizeViaPower;
    public float StarvingViaPower;

}
