using UnityEngine;
using System.Collections;

public class Spawn
{
    public GameObject Alien;
    public Vector2 Position;
    public float SpawnDelay;
    public bool Spawned;
    AlienType _Type;

    public AlienType Type
    {
        get { return _Type; }
    }
    public Spawn(GameObject _Alien, Vector2 _Pos, float _SpawnDelay)
    {
        Alien = _Alien;
        Position = _Pos;
        _Type = Alien.GetComponent<AlienBase>().Type;
        SpawnDelay = _SpawnDelay;
        Spawned = false;
    }
}
public class Wave
{
    public ArrayList _wave;
    public Wave()
    {
        _wave = new ArrayList();
    }
    public void Add(Spawn _spawn)
    {
        _wave.Add(_spawn);
    }
}
public class SpawnData : MonoBehaviour
{
    public Wave[] mWaves;

    public GameObject[] Alien;
    void Awake()
    {
        mWaves = new Wave[2];
        for (int i = 0; i < mWaves.Length; i++)
        {
            mWaves[i] = new Wave();
        }

        mWaves[0].Add(new Spawn(Alien[0], new Vector2(0, 0), 0.5f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(1, 0), 0.5f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(2, 0), 3f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(3, 0), 3f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(4, 0), 3f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(2, 2), 5f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(0, 0), 10f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(1, 0), 10f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(2, 0), 15f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(3, 0), 15f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(4, 0), 15f));
        mWaves[0].Add(new Spawn(Alien[0], new Vector2(2, 2), 15f));


        mWaves[1].Add(new Spawn(Alien[0], new Vector2(0, 0), 0.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(1, 0), 0.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(2, 0), 1.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(3, 0), 1.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(4, 0), 2.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(0, 0), 4.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(1, 0), 4.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(2, 0), 4.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(3, 0), 4.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(4, 0), 4.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(0, 0), 8.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(1, 0), 8.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(2, 0), 8.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(3, 0), 8.5f));
        mWaves[1].Add(new Spawn(Alien[0], new Vector2(4, 0), 8.5f));
        mWaves[1].Add(new Spawn(Alien[1], new Vector2(2, 2), 12.5f));
        mWaves[1].Add(new Spawn(Alien[1], new Vector2(4, 4), 13.5f));
        mWaves[1].Add(new Spawn(Alien[1], new Vector2(0, 4), 13.5f));
        mWaves[1].Add(new Spawn(Alien[1], new Vector2(2, 2), 16.5f));
        mWaves[1].Add(new Spawn(Alien[1], new Vector2(4, 4), 16.5f));
        mWaves[1].Add(new Spawn(Alien[1], new Vector2(0, 4), 16.5f));
    }
}