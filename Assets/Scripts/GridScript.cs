using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour
{
    #region Grid State
    private static bool isGridRotating = false;

    public static bool GridIsTurning()
    {
        return isGridRotating;
    }
    public Transform AlienGrid, AlienFlying, AlienWalking, TowerGrid;
    public float AlienGridRotation, AlienFlyingRotation, AlienWalkingRotation, TowerGridRotation;
    public void RotateAlienGridAll()
    {
        AlienFlyingRotation -= 90;
        StartCoroutine(RotateGrid(AlienFlying, AlienFlyingRotation));
        AlienWalkingRotation -= 90;
        StartCoroutine(RotateGrid(AlienWalking, AlienWalkingRotation));
    }
    public void RotateAlienGridFlying()
    {
        AlienFlyingRotation -= 90;
        StartCoroutine(RotateGrid(AlienFlying, AlienFlyingRotation));
    }
    public void RotateAlienGridWalking()
    {
        AlienWalkingRotation -= 90;
        StartCoroutine(RotateGrid(AlienWalking, AlienWalkingRotation));
    }
    #endregion

    public GameObject AlienOverLord;

    public GameObject TowerPrefab;
    public Transform Cage;
    public int Row;
    public int Column;
    private ArrayList TowerArray;

    private float CurrentRotation = 0;
    // Use this for initialization
    void Awake()
    {


        Material[] materials = new Material[6];
        for (int v = 0; v < materials.Length; v++)
        {
            materials[v] = new Material(TowerPrefab.renderer.material);
        }

        materials[0].color = Color.blue;
        materials[1].color = Color.cyan;
        materials[2].color = Color.green;
        materials[3].color = Color.magenta;
        materials[4].color = Color.red;
        materials[5].color = Color.yellow;

        int i = 0;
        //Row--;
        bool SpawnTowers = false;
        if (SpawnTowers)
        {
            TowerArray = new ArrayList();
            for (int y = 0; y < Column; y++)
            {
                for (int x = 0; x < Row; x++)
                {
                    i = y * Row + x;
                    GameObject temp = Instantiate(TowerPrefab, new Vector3((float)y * 2 - 4, (float)x * 2, 10), TowerPrefab.transform.rotation) as GameObject;
                    TowerArray.Add(temp);

                    //set color
                    temp.renderer.material = materials[Random.Range(0, 5)];

                    temp.transform.parent = TowerGrid;
                }
            }
        }
    }
    public void Move()
    {
                TowerGridRotation -= 90;
                //StartCoroutine(RotateGrid(TowerGrid, TowerGridRotation));
                RotateAlienGridWalking();
                Debug.Log("hit");
    }
    // Update is called once per frame
    public void NewUpdate()
    {

    }

    IEnumerator RotateGrid(Transform _whichGrid, float _rotation)
    {
        isGridRotating = true;
        AllowTowersToMove(false);
        CurrentRotation = _rotation;
        Quaternion DestinationRotation = Quaternion.AngleAxis(CurrentRotation, Vector3.forward);
        do
        {
            _whichGrid.transform.rotation = Quaternion.Lerp(_whichGrid.transform.rotation, DestinationRotation, Time.smoothDeltaTime * 5f);
            yield return new WaitForEndOfFrame();
        }
        while (Quaternion.Angle(_whichGrid.transform.rotation, DestinationRotation) > 0.1f);
        _whichGrid.transform.rotation = DestinationRotation;
        AllowTowersToMove(true);
        isGridRotating = false;
    }
    bool AreAnyTowersMoving()
    {
        // ZY Add your function call here
        //Debug.Log(!Tower.AreTowersStationary() + " " + !isGridRotating);
        Debug.Log("Ext: " + Tower.AreTowersStationary());
        return Tower.AreTowersStationary();
    }
    void AllowTowersToMove(bool active)
    {
        AlienOverLord.GetComponent<AlienOverLord>().AllowAliensToMove(active);
    }
}
