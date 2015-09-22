using UnityEngine;
using System.Collections;

public class CombiScript : MonoBehaviour
{
    public GameObject TowerTempScriptObject;
    public GameObject GridScriptObject;
    GridScript mGS;
    TowersTemp mTT;
    void Awake()
    {
        mGS = GridScriptObject.GetComponent<GridScript>();
        mTT = TowerTempScriptObject.GetComponent<TowersTemp>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Tower.AreTowersStationary())
        {
            mGS.Move();
            mTT.Move();
        }
        mGS.NewUpdate();
        mTT.NewUpdate();
    }
}