using UnityEngine;
using System.Collections;

public class ShopItems : MonoBehaviour
{
    internal Shop Shop;
    internal Renderer Content
    {
        get
        {
            return transform.FindChild("Content").renderer;
        }
    }
    void OnMouseDown()
    {
        Shop.HandleShopItemPressed(int.Parse(name));
    }
}
