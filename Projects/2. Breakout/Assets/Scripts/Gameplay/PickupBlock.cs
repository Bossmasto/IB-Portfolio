using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupBlock : Block
{

    [SerializeField] Sprite freezer;
    [SerializeField] Sprite speedup;

    //type of effect
    PickupEffect effect;

    //Effects description
    float freezeDuration = 5;
    FreezerEffectActivated freezerEffectActivated;

    float speedDuration = 3;
    float speedFactor = 2;
    PickupEffecrtActivated speedEffectActivated;

    // Start is called before the first frame update
    override protected void Start()
    {
        pointsPerBlock = 20;
        base.Start();
    }

    public PickupEffect Effect
    {
        set
        {
            effect = value;

            // set sprite
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (effect == PickupEffect.Freezer)
            {
                spriteRenderer.sprite = freezer;

                //setting up an event
                freezeDuration = ConfigurationUtils.FreezeDuration;
                freezerEffectActivated = new FreezerEffectActivated();
                EventManager.AddFreezerEffectInvoker(this);
            }
            else
            {
                spriteRenderer.sprite = speedup;

                //setting up an event
                speedEffectActivated = new PickupEffecrtActivated();
                EventManager.AddSpeedEffectInvoker(this);

            }
        }
    }

    //Methods to work with events

    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectActivated.AddListener(listener);
    }

    public void AddSpeedEffectListener(UnityAction<float,float> listener)
    {
        speedEffectActivated.AddListener(listener);
    }

     override protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (effect == PickupEffect.Freezer)
            {
                freezerEffectActivated.Invoke(freezeDuration);
            }
            else
            {
                speedEffectActivated.Invoke(speedDuration, speedFactor);
            }
            base.OnCollisionEnter2D(collision);
        }
    }
}
