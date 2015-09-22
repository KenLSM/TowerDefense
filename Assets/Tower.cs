using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    // Physics
    internal bool IsMoving = false;
    internal Vector3 Destination
    {
        get
        {
            return destination;
        }
        set
        {
            destination = value;
            IsMoving = true;
        }
    }
    Vector3 destination = Vector3.zero;
    Vector3 distanceToDestination = Vector3.zero;
    float bouncyness = Random.Range(0.89f, 0.92f);
    float speed = Random.Range(0.48f, 0.52f);
	internal void Step ()
    {
        if (!IsMoving)
        {
            return;
        }
        distanceToDestination += (Destination - transform.localPosition) * speed;

        if (distanceToDestination.magnitude < 0.0005f)
        {
            transform.localPosition = Destination;
            IsMoving = false;
            return;
        }

        distanceToDestination *= bouncyness;
        transform.localPosition += distanceToDestination * Time.smoothDeltaTime;
	}
    public static bool AreTowersStationary()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (TowersTemp.Towers[x, y] != null && TowersTemp.Towers[x, y].IsMoving)
                {
                    Debug.Log("Int: false");
                    return false;
                }
            }
        }
        Debug.Log("Int: True");
        return true;
    }
    // end Physics

    GameObject Base;
    GameObject Body;
    GameObject Turret;
    void Awake()
    {
        Base = transform.FindChild("CenterPoint").FindChild("Base").gameObject;
        Body = Base.transform.FindChild("Body").gameObject;
        Turret = Body.transform.FindChild("Turret").gameObject;
    }
    internal void ChangeBaseTo(Color color)
    {
        Base.renderer.material.color = color;
    }
    internal void ChangeBodyTo(Color color)
    {
        Body.renderer.material.color = color;
    }
    internal void ChangeTurretTo(Color color)
    {
        Turret.renderer.material.color = color;
    }
}
