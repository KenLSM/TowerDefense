using UnityEngine;
using System.Collections;

public class AlienOverLord : MonoBehaviour
{
    Array2D mArray = new Array2D(5, 5);

    public GameObject[] Alien;
    public Transform AlienGrid, AlienFlying, AlienWalking, TowerGrid;
    ArrayList AlienArray;

    public void CreateAlien(GameObject _whichAlien, int _atRow, int _atCol)
    {
        GameObject _temp = Instantiate(_whichAlien, CalculateRowCol(_atRow, _atCol), _whichAlien.transform.rotation) as GameObject;
        AlienArray.Add(_temp);

        if (_whichAlien.GetComponent<AlienBase>().Type == AlienType.Fly)
        {
            _temp.transform.parent = AlienFlying;
        }
        else
        {
            _temp.transform.parent = AlienWalking;
        }
    }
    Vector3 CalculateRowCol(int _atRow, int _atCol)
    {
        return new Vector3((float)_atRow * 1, (float)_atCol * 1, -17);
    }
    void Awake()
    {
        int counter = 1;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mArray.Array[i][j] = counter++;
            }
        }
        mSpawnData = this.gameObject.GetComponent<SpawnData>();
        mSpawnState = SpawnState.Countdown;
        SpawnCounter = SpawnTime;
        AlienArray = new ArrayList();

        //for (int i = 0; i < 5; i++)
        //{
        //    CreateAlien(0, i, 1);
        //    CreateAlien(0, i, 2);
        //}
        //CreateAlien(0, 3, 4);
    }

    public bool AnyAlive()
    {
        return AlienArray.Count > 0;
    }
    void Update()
    {
        Singleton.Get().SingletonUpdate();

        // Removal of dead aliens
        for (int i = 0; i < AlienArray.Count; )
        {
            if (AlienArray[i] as GameObject == null)
            {
                AlienArray.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        SpawnUpdate();
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    mArray.TransposeAntiClockwise();
        //}
        //string a = "";
        //for (int i = 0; i < 5; i++)
        //{
        //    for (int j = 0; j < 5; j++)
        //    {
        //        a += mArray.Array[i][j];
        //    }
        //    a += "\n";
        //}
        //Debug.Log(a);
    }

    public void AllowAliensToMove(bool active)
    {
        for (int i = 0; i < AlienArray.Count; )
        {
            if (AlienArray[i] as GameObject == null)
            {
                AlienArray.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        foreach (GameObject alien in AlienArray)
        {
            alien.rigidbody.isKinematic = !active;
            if (alien.GetComponent<AlienBase>() != null)
                alien.GetComponent<AlienBase>().CanMove = active;
        }
    }

    #region Spawning

    SpawnData mSpawnData;

    enum SpawnState
    {
        Countdown, Spawning, Waiting, End
    }
    SpawnState mSpawnState;

    float SpawnCounter;
    float SpawnTime = 3;
    int SpawnCurrentWave = 0;
    void SpawnUpdate()
    {
        switch (mSpawnState)
        {
            case SpawnState.Countdown:

                SpawnCounter -= Time.deltaTime;
                if (SpawnCounter < 0)
                {

                    mSpawnState = SpawnState.Spawning;
                }
                break;
            case SpawnState.Spawning:

                Wave _curWave = mSpawnData.mWaves[SpawnCurrentWave];
                bool allSpawned = true;

                // Spawning
                for (int i = 0; i < _curWave._wave.Count; i++)
                {
                    Spawn _s = _curWave._wave[i] as Spawn;
                    _s.SpawnDelay -= Time.deltaTime;
                    if (_s.SpawnDelay < 0)
                    {
                        if (_s.Spawned == false)
                        {
                            CreateAlien(_s.Alien, (int)_s.Position.x, (int)_s.Position.y);
                            _s.Spawned = true;
                        }
                    }
                    else
                    {
                        allSpawned = false;
                    }
                }

                // Proceed to next state if all spawned
                if (allSpawned)
                { 
                    mSpawnState = SpawnState.Waiting;
                }
                break;
            case SpawnState.Waiting:
                if (AnyAlive() == false)
                {
                    SpawnCounter = SpawnTime;
                    SpawnCurrentWave++;
                    if (SpawnCurrentWave > mSpawnData.mWaves.Length - 1)
                    {
                        mSpawnState = SpawnState.End;
                    }
                    mSpawnState = SpawnState.Countdown;
                }
                break;
        }
    }
    #endregion
}