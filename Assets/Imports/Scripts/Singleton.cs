using UnityEngine;
using System.Collections;

public class Singleton
{
    static Singleton self;

    private Singleton()
    {

    }

    public static Singleton Get()
    {
        if (self == null)
        {
            self = new Singleton();
        }
        return self;
    }

    
    public void SingletonUpdate()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
    }
}