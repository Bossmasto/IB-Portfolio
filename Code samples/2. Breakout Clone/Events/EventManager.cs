using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{

    //Fields for Freezer effects
    static List<PickupBlock> invokerList = new List<PickupBlock>();
    static List<UnityAction<float>> listenerList = new List<UnityAction<float>>();

    //fields for speedup effects
    static List<PickupBlock> speedInvokerList = new List<PickupBlock>();
    static List<UnityAction<float, float>> speedListenerList = new List<UnityAction<float, float>>();

    //fields for Addpoint event
    static List<Block> pointsInvokerLIst = new List<Block>();
    static List<UnityAction<int>> pointsListenerList = new List<UnityAction<int>>();

    //fields for reducing ball count
    static List<Ball> ballInvokerList = new List<Ball>();
    static List<UnityAction> ballListenerList = new List<UnityAction>();

    // LastBallLost support
    static List<HUD> lastBallLostInvokers = new List<HUD>();
    static List<UnityAction> lastBallLostListeners =
        new List<UnityAction>();

    // LastBlockBroken support
    static List<Block> lastBlockBrokenInvokers = new List<Block>();
    static List<UnityAction> lastBlockBrokenListeners =
        new List<UnityAction>();



    //Methods to add listeners and invokers to the events in the manager

    public static void AddFreezerEffectInvoker(PickupBlock invoker)
    {
        invokerList.Add(invoker);
        foreach(UnityAction<float> listener in listenerList)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    public static void AddFreezerEffectListener(UnityAction<float> handler)
    {
        listenerList.Add(handler);
        foreach(PickupBlock pickup in invokerList)
        {
            pickup.AddFreezerEffectListener(handler);
        }
    }

    public static void AddSpeedEffectInvoker (PickupBlock invoker)
    {
        speedInvokerList.Add(invoker);
        foreach (UnityAction<float,float> listener in speedListenerList)
        {
            invoker.AddSpeedEffectListener(listener);
        }
    }

    public static void AddSpeedEffectListener (UnityAction<float, float> listener)
    {
        speedListenerList.Add(listener);
        foreach (PickupBlock block in speedInvokerList)
        {
            block.AddSpeedEffectListener(listener);
        }
    }

    public static void AddPointsInvoker(Block invoker)
    {
        pointsInvokerLIst.Add(invoker);
        foreach (UnityAction<int> listener in pointsListenerList)
        {
            invoker.AddPointsActivatedListener(listener);
        }
    }

    public static void AddPointsListener (UnityAction<int> listener)
    {
        pointsListenerList.Add(listener);
        foreach(Block block in pointsInvokerLIst)
        {
            block.AddPointsActivatedListener(listener);
        }
    }

    public static void BallCountInvoker(Ball invoker)
    {
        ballInvokerList.Add(invoker);
        foreach (UnityAction listener in ballListenerList)
        {
            invoker.ReduceBallCountListener(listener);
        }
    }

    public static void BallCountListener(UnityAction listener)
    {
        ballListenerList.Add(listener);
        foreach (Ball ball in ballInvokerList)
        {
            ball.ReduceBallCountListener(listener);
        }
    }

    public static void AddLastBallLostInvoker(HUD invoker)
    {
        // add invoker to list and add all listeners to invoker
        lastBallLostInvokers.Add(invoker);
        foreach (UnityAction listener in lastBallLostListeners)
        {
            invoker.AddLastBallLostListener(listener);
        }
    }

    public static void AddLastBallLostListener(UnityAction listener)
    {
        // add listener to list and to all invokers
        lastBallLostListeners.Add(listener);
        foreach (HUD invoker in lastBallLostInvokers)
        {
            invoker.AddLastBallLostListener(listener);
        }
    }

    public static void AddLastBlockBrokenInvoker(Block invoker)
    {
        // add invoker to list and add all listeners to invoker
        lastBlockBrokenInvokers.Add(invoker);
        foreach (UnityAction listener in lastBlockBrokenListeners)
        {
            invoker.AddLastBlockBrokenListener(listener);
        }
    }

    public static void AddLastBlockBrokenListener(UnityAction listener)
    {
        // add listener to list and to all invokers
        lastBlockBrokenListeners.Add(listener);
        foreach (Block invoker in lastBlockBrokenInvokers)
        {
            invoker.AddLastBlockBrokenListener(listener);
        }
    }
}
