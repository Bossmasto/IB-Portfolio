using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]GameObject player;

    float zoomedIn = 20f; //camera view
    float zoomedInSensitivity = .5f; //sensitivity when zoomed in

    float zoomedOut = 60f; // camera view
    float zoomedOutSensitivity = 2f;//sensitivity when zoomed out

    bool zoomedInFlag = false;


    private void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {

        //zooming in and out using mouse button and toggle
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomedInFlag)
            {
                zoomedInFlag = true;
                mainCamera.fieldOfView = zoomedIn;
            }
            else
            {
                zoomedInFlag = false;
                mainCamera.fieldOfView = zoomedOut;
            }
        }        
    }
}
