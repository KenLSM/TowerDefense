using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    public Camera GUICamera;
    enum ShopMode
    {
        InCategory,
        InBase,
        InBody,
        InTurret,
        InInner,
        InOuter
    }
    ShopMode CurrentShopMode = ShopMode.InCategory;

    public Transform MouseFocus;
    public AnimationCurve TileFocusCurve;
    public AnimationCurve TileMouseOverCurve;

    public Transform ShopItemParent;
    public ShopItems[] ShopItem;

    public Material BackMaterial;

    public Material[] BaseMaterial;
    public Material[] BodyMaterial;
    public Material[] TurretMaterial;
    public Material[] InnerAccessoryMaterial;
    public Material[] OuterAccessoryMaterial;

    public GameObject ShopSlotPrefab;
    ShopSlots[,] ShopSlotsArray = new ShopSlots[5, 5];

    internal void AddTower(GameObject miniTower, int x, int y, Tower linkedTo)
    {
        ShopSlotsArray[x, y].SlotContent = miniTower;
        ShopSlotsArray[x, y].ActualTower = linkedTo.gameObject;
    }
    internal void RemoveTower(int x, int y)
    {
        Destroy(ShopSlotsArray[x, y].SlotContent);
        ShopSlotsArray[x, y].SlotContent = null;
        ShopSlotsArray[x, y].ActualTower = null;
    }
    internal void RemoveAllTowers()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                RemoveTower(x, y);
            }
        }
    }
    internal void MoveTower(int fromX, int fromY, int toX, int toY)
    {
        ShopSlotsArray[toX, toY].SlotContent = ShopSlotsArray[fromX, fromY].SlotContent;
        ShopSlotsArray[fromX, fromY].SlotContent = null;
        ShopSlotsArray[toX, toY].ActualTower = ShopSlotsArray[fromX, fromY].ActualTower;
        ShopSlotsArray[fromX, fromY].ActualTower = null;
    }

    public static bool isVisible = false;
    void ToggleVisibility()
    {
        isVisible = !isVisible;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActiveRecursively(isVisible);
        }
        if (isVisible)
        {
            foreach (ShopItems item in ShopItem)
            {
                item.gameObject.SetActiveRecursively(false);
            }
        }
    }

    void Awake()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                ShopSlotsArray[x, y] = (Instantiate(ShopSlotPrefab) as GameObject).GetComponent<ShopSlots>();
                ShopSlotsArray[x, y].transform.parent = transform;
                ShopSlotsArray[x,y].Init(new Vector3((x-2) * 10, (y-2) * 10, -5), new Vector3(20, 20, 20), this);
                ShopSlotsArray[x, y].transform.localRotation = new Quaternion(0, 0.5f, 0, 0);
            }
        }
        foreach (ShopSlots slot in transform.GetComponentsInChildren<ShopSlots>())
        {
            slot.TileFocusCurve = TileFocusCurve;
            slot.TileMouseOverCurve = TileMouseOverCurve;
            slot.ShopItems = ShopItemParent;
        }
        foreach (ShopItems item in ShopItem)
        {
            item.Shop = this;
        }
    }
    void Start()
    {
        ShopItem[0].Content.material = BaseMaterial[0];
        ShopItem[1].Content.material = BodyMaterial[0];
        ShopItem[2].Content.material = TurretMaterial[0];
        ShopItem[3].Content.material = InnerAccessoryMaterial[0];
        ShopItem[4].Content.material = OuterAccessoryMaterial[0];


    }
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleVisibility();
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 8;
        MouseFocus.transform.position = Vector3.Lerp(MouseFocus.transform.position, GUICamera.ScreenToWorldPoint(mousePosition) * 0.5f + Vector3.right * 3f + Vector3.down, Time.smoothDeltaTime * 5f);
        transform.LookAt(MouseFocus);
	}
    internal void HandleShopItemPressed(int index)
    {
        switch (CurrentShopMode)
        {
            case ShopMode.InCategory:
            {
                ShopItem[0].Content.material = BackMaterial;
                switch (index)
                {
                    case 0:
                    {
                        CurrentShopMode = ShopMode.InBase;
                        for (int i = 1; i < 5; i++)
                        {
                            ShopItem[i].Content.material = BaseMaterial[i - 1];
                        }
                    }
                    break;
                    case 1:
                    {
                        CurrentShopMode = ShopMode.InBody;
                        for (int i = 1; i < 5; i++)
                        {
                            ShopItem[i].Content.material = BodyMaterial[i - 1];
                        }
                    }
                    break;
                    case 2:
                    {
                        CurrentShopMode = ShopMode.InTurret;
                        for (int i = 1; i < 5; i++)
                        {
                            ShopItem[i].Content.material = TurretMaterial[i - 1];
                        }
                    }
                    break;
                    case 3:
                    {
                        CurrentShopMode = ShopMode.InInner;
                        for (int i = 1; i < 5; i++)
                        {
                            ShopItem[i].Content.material = InnerAccessoryMaterial[i - 1];
                        }
                    }
                    break;
                    case 4:
                    {
                        CurrentShopMode = ShopMode.InOuter;
                        for (int i = 1; i < 5; i++)
                        {
                            ShopItem[i].Content.material = OuterAccessoryMaterial[i - 1];
                        }
                    }
                    break;
                }
            }
            break;
            case ShopMode.InBase:
            {
                switch(index)
                {
                    case 0:
                    {
                            ResetShopItems();
                    }
                    break;
                    case 1:
                    {
                        ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBaseTo(Color.black);
                        ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBaseTo(Color.black);
                    }
                    break;
                    case 2:
                    {
                        ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBaseTo(Color.green);
                        ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBaseTo(Color.green);
                    }
                    break;
                    case 3:
                    {
                        ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBaseTo(Color.red);
                        ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBaseTo(Color.red);
                    }
                    break;
                    case 4:
                    {
                        ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBaseTo(Color.cyan);
                        ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBaseTo(Color.cyan);
                    }
                    break;
                }
            }
            break;
            case ShopMode.InBody:
            {
                switch (index)
                {
                    case 0:
                        {
                            ResetShopItems();
                        }
                        break;
                    case 1:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBodyTo(Color.black);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBodyTo(Color.black);
                        }
                        break;
                    case 2:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBodyTo(Color.green);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBodyTo(Color.green);
                        }
                        break;
                    case 3:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBodyTo(Color.red);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBodyTo(Color.red);
                        }
                        break;
                    case 4:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeBodyTo(Color.cyan);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeBodyTo(Color.cyan);
                        }
                        break;
                }
            }
            break;
            case ShopMode.InTurret:
            {
                switch (index)
                {
                    case 0:
                        {
                            ResetShopItems();
                        }
                        break;
                    case 1:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeTurretTo(Color.black);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeTurretTo(Color.black);
                        }
                        break;
                    case 2:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeTurretTo(Color.green);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeTurretTo(Color.green);
                        }
                        break;
                    case 3:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeTurretTo(Color.red);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeTurretTo(Color.red);
                        }
                        break;
                    case 4:
                        {
                            ShopSlots.ShopItemsHookedTo.SlotContent.GetComponent<Tower>().ChangeTurretTo(Color.cyan);
                            ShopSlots.ShopItemsHookedTo.ActualTower.GetComponent<Tower>().ChangeTurretTo(Color.cyan);
                        }
                        break;
                }
            }
            break;
            default:
            {
                if (index == 0)
                {
                    ResetShopItems();
                }
            }
            break;
        }
    }
    internal void ResetShopItems()
    {
        CurrentShopMode = ShopMode.InCategory;
        ShopItem[0].Content.material = BaseMaterial[0];
        ShopItem[1].Content.material = BodyMaterial[0];
        ShopItem[2].Content.material = TurretMaterial[0];
        ShopItem[3].Content.material = InnerAccessoryMaterial[0];
        ShopItem[4].Content.material = OuterAccessoryMaterial[0];
    }
}
