using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils

{

    static SpeedupEffectMonitor speedupEffect = Camera.main.GetComponent<SpeedupEffectMonitor>();

    public static bool EffectRunning 
    {
        get
        {
            return speedupEffect.SpeedupEffectActive;
        }
    }

    public static float SpeedupFactor
    {
        get { return speedupEffect.SpeedupFactor; }
    }
}
