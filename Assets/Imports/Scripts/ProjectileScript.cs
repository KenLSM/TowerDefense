using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    public float Damage;
    public float MoveSpeed;

    void OnTriggerEnter(Collider co)
    {
        AlienBase script = co.gameObject.GetComponent<AlienBase>();
        if (script != null)
        {
            script.Health -= Damage;
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(GridScript.GridIsTurning() == false)
             this.gameObject.transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime, Space.Self);

        if (this.gameObject.transform.position.x > 20)
        {
            Destroy(this.gameObject);
        }
    }
}