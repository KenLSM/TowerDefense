using UnityEngine;
using System.Collections;

/*
 * Delayed fade class
 * Extends fadescript
 * 
 * Allows the fading to be delayed by X seconds
 */
public class DelayedFadeScript : FadeScript
{
    public float DelayAmount;
    public float delayCounter;

    override public void Awake()
    {
        delayCounter = DelayAmount;
        base.Awake();
    }
    override public void StartFadeOut()
    {
        delayCounter = 0;
        base.StartFadeOut();
    }
    override public void StartFadeIn()
    {
        delayCounter = 0;
        base.StartFadeIn();
    }
    override public void Update()
    {
        if (delayCounter > DelayAmount)
        {
            base.Update();
        }
        else
        {
            delayCounter += Time.deltaTime;
        }
    }
}
