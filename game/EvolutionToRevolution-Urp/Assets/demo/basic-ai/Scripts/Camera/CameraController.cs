using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {

        //Rotation
        float rotation = 0;
        if (Input.GetKey(KeyCode.Q))
            rotation -= 1;
        if (Input.GetKey(KeyCode.E))
            rotation += 1;
        transform.Rotate(0, rotation, 0);
    }
}

