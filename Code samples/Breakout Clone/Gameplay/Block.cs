using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Block breaking and other actions related to blocks in the game
/// </summary>
public class Block : MonoBehaviour
{

    protected int pointsPerBlock = 1;

    //events
    AddPointsActivated addPointsActivated;
    LastBlockBroken lastBlockBroken;

    protected virtual void Start()
    {
        gameObject.tag = "Block";
        addPointsActivated = new AddPointsActivated();
        lastBlockBroken = new LastBlockBroken();
        EventManager.AddPointsInvoker(this);
        EventManager.AddLastBlockBrokenInvoker(this);
    }
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            addPointsActivated.Invoke(pointsPerBlock);
            lastBlockBroken.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddPointsActivatedListener(UnityAction<int> listener)
    {
        addPointsActivated.AddListener(listener);
    }

    public void AddLastBlockBrokenListener(UnityAction listener)
    {
        lastBlockBroken.AddListener(listener);
    }
}

