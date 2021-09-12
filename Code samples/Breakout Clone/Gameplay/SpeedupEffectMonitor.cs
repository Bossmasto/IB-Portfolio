using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupEffectMonitor : MonoBehaviour
{

    Timer speedTimer;
    float factor = 2;

    //Gets whether the speedup effective is active
    public bool SpeedupEffectActive
    {
        get { return speedTimer.Running; }
    }

    public float SpeedupFactor
    {
        get { return factor; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //speedup effect work
        speedTimer = gameObject.AddComponent<Timer>();
        EventManager.AddSpeedEffectListener(OnSpeedUp);
        speedTimer.AddTimerFinishedListener(OnSpeedTimerFinished);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSpeedTimerFinished()
    {
        speedTimer.Stop();
        factor = 1;
    }

    void OnSpeedUp(float duration, float factor)
    {
        // run or add time to timer
        if (!speedTimer.Running)
        {
            this.factor =factor;
            speedTimer.Duration = duration;
            speedTimer.Run();
        }
        else
        {
            speedTimer.IncreaseDuration(duration);
        }
    }
}
