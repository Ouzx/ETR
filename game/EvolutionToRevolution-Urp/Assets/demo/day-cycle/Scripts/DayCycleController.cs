using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DayCycleController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 24)]
    private float timeOfDay;

    [SerializeField]
    private float orbitSpeed = 1.0f;

    [SerializeField] private GameObject BlenderObject;
    private SkyboxBlender Blender;
    private bool blend = false;
    [SerializeField] private float blendSpeed = 0.2f;

    [SerializeField] private Material[] Skyboxes;
    [SerializeField] private Material[] BlendedSkyboxes;

    private byte[] transitionTimes = { 6, 9, 15, 18, 0 };
    private byte currentTranstion = 9;

    private void Start()
    {
        Blender = BlenderObject.GetComponent<SkyboxBlender>();
    }

    private void FixedUpdate()
    {
        timeOfDay += orbitSpeed;
        if (timeOfDay > 24) timeOfDay = 0;
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        RenderSettings.skybox.SetFloat("_Rotation", sunRotation);
        Blender.rotation = sunRotation;
        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (timeOfDay >= transitionTimes[0] && timeOfDay <= transitionTimes[1])
        {
            if (currentTranstion != 0) Translate(0, 1);
            CheckBlending(transitionTimes[1]);
        }
        else if (timeOfDay >= transitionTimes[1] && timeOfDay <= transitionTimes[2])
        {
            if (currentTranstion != 1) Translate(1, 2);
            CheckBlending(transitionTimes[2]);
        }
        else if (timeOfDay >= transitionTimes[2] && timeOfDay <= transitionTimes[3])
        {
            if (currentTranstion != 2) Translate(2, 3);
            CheckBlending(transitionTimes[3]);
        }
        else if (timeOfDay >= transitionTimes[3] && timeOfDay <= transitionTimes[4])
        {
            if (currentTranstion != 3) Translate(3, 4);
            CheckBlending(transitionTimes[4]);
        }
        else if (timeOfDay >= transitionTimes[4] && timeOfDay <= transitionTimes[0])
        {
            if (currentTranstion != 4) Translate(4, 0);
            CheckBlending(transitionTimes[0]);
        }
    }

    private void CheckBlending(byte transiton)
    {
        float delta = transiton - timeOfDay;
        if (delta <= 0.5)
        {
            Blender.blend += blendSpeed;
            if (Blender.blend <= 1)
            {
                Blender.Blend();
            }
        }
    }

    // private void Blend()
    // {
    //     if (blend)
    //     {
    //         currentBlending += blendSpeed;
    //         if (currentBlending <= 1)
    //         {
    //             Blender.blend = currentBlending;
    //             Blender.Blend();
    //         }
    //         else blend = false;
    //     }
    // }

    private void Translate(byte index, byte index2)
    {
        currentTranstion = index;
        // RenderSettings.skybox = BlendedSkyboxes[index];
        // Blender.blendedSkybox = BlendedSkyboxes[index];
        Blender.skyBox1 = Skyboxes[index];
        Blender.skyBox2 = Skyboxes[index2];
        RenderSettings.skybox = Blender.blendedSkybox;
        Blender.blend = 0;
        Blender.Refresh();
    }

}

