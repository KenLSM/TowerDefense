using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour
{
    public GameObject Projectile;
    public float Attack_CoolDown;

    private float counter;
    void Start()
    {
        counter = Attack_CoolDown;
    }

    void LateUpdate()
    {
        if (this.transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Attack_CoolDown -= 1;
            counter = Attack_CoolDown;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Attack_CoolDown += 1;
            counter = Attack_CoolDown;
        }
        if (Attack_CoolDown < 0.1f)
        {
            Attack_CoolDown = 0.1f;
        }


        if (GridScript.GridIsTurning() == false)
            counter -= Time.deltaTime;

        if (Tower.AreTowersStationary())
        {
            TryFire();
        }

    }

    public void TryFire()
    {
        if (counter < 0)
        {
            counter = Attack_CoolDown;
            (Instantiate(Projectile) as GameObject).transform.position = this.gameObject.transform.position;
        }
    }
}