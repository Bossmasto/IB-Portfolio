using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    static float paddleMoveUnitsPerSecond = 10;
    static float ballImpulseForce = 200;
    static float ballLifeTime = 10;
    static int minSpawn= 5;
    static int maxSpawn = 10;
    static int numberOfBalls = 5;

    static float freezerDuration = 3;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }


    //Setting up BallLifeTime
    public float BallLifeTime 

    { 
        get { return ballLifeTime; } 
    }
        public int MinSpawn 
    { 
        get { return minSpawn; } 
    }
        public int MaxSpawn
    { 
        get { return maxSpawn; } 
    }    
        
        public int NumberOfBalls
    { 
        get { return numberOfBalls; } 
    }     
    
    public float FreezeDuration
    { 
        get { return freezerDuration; } 
    }


    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader streamReader = null ;

        try
        {
            // create stream reader object
            streamReader = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            // read in names and values
            string names = streamReader.ReadLine();
            string values = streamReader.ReadLine();

            // set configuration data fields
            SetConfigurationDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            // always close input file
            streamReader?.Close();
        }
    }
    void SetConfigurationDataFields(string csvValues)
    {
        // the code below assumes we know the order in which the
        // values appear in the string. We could do something more
        // complicated with the names and values, but that's not
        // necessary here
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
        ballLifeTime = float.Parse(values[2]);
        minSpawn = int.Parse(values[3]);
        maxSpawn = int.Parse(values[4]);
        numberOfBalls = int.Parse(values[5]);
    }

    #endregion
}
