using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    void LateUpdate()
    {

        //Rotation
        float rotation = 0;
        if (Input.GetKey(KeyCode.D))
            rotation -= 1;
        if (Input.GetKey(KeyCode.A))
            rotation += 1;
        transform.Rotate(0, rotation, 0);
    }
}

