using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Cost Coefficients", menuName = "Coefficients/Costs")]
public class CostCoefficients : ScriptableObject
{
    public float Starving;
    public float Walking;
    public float Attacking;
    public float Tiring;
    public float Night;

}
