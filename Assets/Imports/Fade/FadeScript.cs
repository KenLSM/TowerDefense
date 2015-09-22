using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * Fade class
 * Helps to fade an object
 * Visible() - Is the object now fully visible?
 * NotVisible() - Is the object now fully invisible?
 * On() - Set to Visible
 * Off() - Set to Invisible
 * StartFadeOut() - Starts fading out process
 * StartFadeIn() - Starts fading in process
 */
public class FadeScript : MonoBehaviour
{
    public Boolean? startFade;
    public Boolean? completed;
    public Boolean? fadingOut;
    public float duration;
    public float index = 0;
    Color mCol;
    // Use this for initialization
    public virtual void Awake()
    {
        startFade = null;
        completed = null;
        fadingOut = null;
        mCol = this.gameObject.renderer.material.color;
        index = 0;
        duration = 1 / duration;
    }
    public bool NotVisible()
    {
        return mCol.a == 0.0f;
    }
    public bool Visible()
    {
        return mCol.a == 1.0f;
    }
    public void Off()
    {
        mCol = this.gameObject.renderer.material.color;
        mCol.a = 0;
        gameObject.renderer.material.color = mCol;
    }
    public void On()
    {
        mCol = this.gameObject.renderer.material.color;
        mCol.a = 1.0f;
        gameObject.renderer.material.color = mCol;
    }
    public virtual void StartFadeOut()
    {
        mCol = this.gameObject.renderer.material.color;
        startFade = true;
        completed = false;
        fadingOut = true;
        index = mCol.a;
    }
    public virtual void StartFadeIn()
    {
        mCol = this.gameObject.renderer.material.color;
        startFade = true;
        completed = false;
        fadingOut = false;
        index = mCol.a;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (startFade == true && !completed == true)
        {
            if (fadingOut == true)
            {
                mCol.a -= Time.deltaTime * duration;
                if (mCol.a < 0)
                {
                    completed = true;
                    mCol.a = 0;                    
                }
                gameObject.renderer.material.color = mCol;
            }
            else
            {
                mCol.a += Time.deltaTime * duration;
                if (gameObject.renderer.material.color.a > 1.0f)
                {
                    completed = true;
                    mCol.a = 1.0f;
                } 
                gameObject.renderer.material.color = mCol;
            }
        }
    }
}