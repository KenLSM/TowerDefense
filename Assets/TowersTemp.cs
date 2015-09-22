using UnityEngine;
using System.Collections;

public class TowersTemp : MonoBehaviour
{
    public GameObject TowerPrefab;
    public static Matrix Towers = null;
    public Shop Shop;
    float timer = 0;
	// Use this for initialization
	void Awake ()
    {
        Tower[,] tempTowers = new Tower[5, 5];
        for (int x = 0; x < 5; x++)
        {
            for (int y = 2; y < 5; y++)
            {
                tempTowers[x, y] = (Instantiate(TowerPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject).GetComponent<Tower>();
                tempTowers[x, y].transform.parent = transform;
                tempTowers[x, y].Destination = new Vector3(x, y, 0);
                Shop.AddTower(Instantiate(TowerPrefab) as GameObject, x, y, tempTowers[x, y]);
            }
        }
        Towers = new Matrix(tempTowers);
	}
    public void Move()
    {
        Shop.RemoveAllTowers();
        Debug.Log(Towers.ToString());
        Towers.RotateClockwise90();
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (Towers[x, y] == null)
                    continue;

                Towers[x, y].name = "[" + x + ", " + y + "]";
                Towers[x, y].Destination = new Vector3(x, y);
                Shop.AddTower(Instantiate(Towers[x, y].gameObject) as GameObject, x, y, Towers[x, y]);
            }
        }
    }
    // Update is called once per frame
    public void NewUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Tower.AreTowersStationary())
        {
           
        }
        else if (Input.GetKeyDown(KeyCode.Delete) && ShopSlots.ShopItemsHookedTo.ActualTower != null)
        {
            ShopSlots.ShopItemsHookedTo.DeleteTower();
        }
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (Towers[x, y] == null)
                    continue;

                Towers[x, y].Step();

                if (timer > 0.01f)
                {
                    int below = y - 1;
                    if (below >= 0 && Towers[x, below] == null)
                    {
                        Towers[x, below] = Towers[x, y];
                        Towers[x, y] = null;
                        Towers[x, below].name = "[" + x + ", " + below + "]";
                        Towers[x, below].Destination = new Vector3(x, below);
                        timer = 0;

                        Shop.MoveTower(x, y, x, below);
                    }
                }
            }
        }
        timer += Time.deltaTime;
	}
}
