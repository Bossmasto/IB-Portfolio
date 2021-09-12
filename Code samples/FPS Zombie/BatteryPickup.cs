using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float extraIntensity = 2f;
    [SerializeField] float newAngle = 70f;

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// changing angle and intensity of the flashlight
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.GetComponentInChildren<FlashlightSystem>().RestoreAngle(newAngle);
            other.GetComponentInChildren<FlashlightSystem>().RestoreIntensity(extraIntensity);
            Destroy(gameObject);
        }
    }
}
