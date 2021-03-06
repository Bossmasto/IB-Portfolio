using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    static ConfigurationData configurationData;

    #region Properties
    
    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    { 
        get { return configurationData.PaddleMoveUnitsPerSecond; }
    }

    public static float BallImpulseForce { get { return configurationData.BallImpulseForce; } }
    public static float BallLifeTime { get { return configurationData.BallLifeTime; } }
    public static int minSpawn { get { return configurationData.MinSpawn; } }
    public static int MaxSpawn { get { return configurationData.MaxSpawn; } }
    public static int NumberOfBalls { get { return configurationData.NumberOfBalls; } }
    public static float FreezeDuration { get { return configurationData.FreezeDuration; } }



    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
