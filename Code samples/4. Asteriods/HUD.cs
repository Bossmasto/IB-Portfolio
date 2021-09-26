using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    [SerializeField] Text text;
    float elapsedTime = 0;

    //timer
    bool isRunning = true;


    // Start is called before the first frame update
    void Start()
    {
        text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            int time = (int)elapsedTime;

            text.text = time.ToString();
        }
    }

    public void StopGameTimer()
    {
        isRunning = false;
    }
}
