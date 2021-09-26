using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] float lightDrop = 0.1f;
    [SerializeField] float angleDrop = 1f;
    [SerializeField] float minAngle = 40f;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreAngle(float newAngle)
    {
        myLight.spotAngle = newAngle;
    }    
    
    public void RestoreIntensity(float newIntensity)
    {
        myLight.spotAngle += newIntensity;
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDrop*Time.deltaTime;
    }

    private void DecreaseLightAngle()
    {

        if(myLight.spotAngle <= minAngle) { return; }

        myLight.spotAngle -= angleDrop*Time.deltaTime;
    }
}
