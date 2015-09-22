using UnityEngine;
using System.Collections;

public enum AlienType
{
    Basic, Walk, Fly
}
public class AlienBase : MonoBehaviour
{
    public float MoveSpeed;
    public bool CanMove;
    public float Health;

    [SerializeField]
    public AlienType Type;

    bool isDead = false;
    void Awake()
    {
        switch (Type)
        {
            case AlienType.Fly:
                {
                    this.gameObject.rigidbody.useGravity = false;
                }
                break;
            case AlienType.Basic:
                {
                    this.gameObject.rigidbody.useGravity = true;
                }
                break;
            case AlienType.Walk:
                {
                    this.gameObject.rigidbody.useGravity = true;
                } 
                break;
        }

        MoveSpeed /= 10;
        if (this.gameObject.GetComponent("FadeScript") == null)
        {
            (this.gameObject.AddComponent("FadeScript") as FadeScript).duration = 0.5f;
        }

    }
    void Update()
    {
        if (CanMove && Mathf.Abs(rigidbody.velocity.y) < 0.1f && GridScript.GridIsTurning() == false)
        {
            this.gameObject.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
        }
        if (Health < 0 && !isDead)
        {
            this.gameObject.GetComponent<FadeScript>().StartFadeOut();
            Destroy(this);
            Destroy(this.gameObject, 1);
            isDead = true;
        }
    }
}