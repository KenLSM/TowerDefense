using UnityEngine;
using System.Collections;

public class AlienCollisionScript : MonoBehaviour
{
    void OnTriggerEnter(Collider co)
    {
        
        AlienBase script = co.gameObject.GetComponent<AlienBase>();
        if (script != null)
        {
            if (script.Type == AlienType.Fly)
            {
                script.Health -= script.Health + 1;
            }
        }
    }
}