using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    PlayerMotor motor;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }
    void Update()
    {
        motor.Patrol();
    }
}
