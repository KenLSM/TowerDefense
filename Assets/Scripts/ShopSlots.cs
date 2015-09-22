using UnityEngine;
using System.Collections;

public class ShopSlots : MonoBehaviour
{
    // GUI
    internal AnimationCurve TileFocusCurve;
    internal AnimationCurve TileMouseOverCurve;
    internal Transform ShopItems;
    public static ShopSlots ShopItemsHookedTo = null;
    static ShopSlots MouseHoveringOver = null;
    float time = 0;
    float curveEvalution = 0;
    Vector3 initialPosition;
    Vector3 initialScale;
    Color slotColor = Color.white;
    Shop shopInstance;
    internal void Init(Vector3 _initialPosition, Vector3 _initialScale, Shop _shopInstance)
    {
        transform.localPosition = initialPosition = _initialPosition;
        transform.localScale = initialScale = _initialScale;
        shopInstance = _shopInstance;
    }
    void Awake()
    {
        renderer.material = new Material(renderer.material);
        slotColor.a = 0.08f;
        renderer.material.SetColor("_TintColor", slotColor);
    }
    void Start()
    {
        gameObject.SetActiveRecursively(false);
    }
    void SlotAnimateAt(float time)
    {
        curveEvalution = TileFocusCurve.Evaluate(time);
        transform.localPosition = initialPosition + Vector3.back * curveEvalution * 2f;
        slotColor.a = Mathf.Clamp(curveEvalution * 0.3f, 0.08f, 0.3f);
        renderer.material.SetColor("_TintColor", slotColor);
        transform.localScale = initialScale + Vector3.right * curveEvalution * 5f + Vector3.up * curveEvalution * 5f;
    }
	IEnumerator OnMouseEnter()
    {
        if (ShopItemsHookedTo != this)
        {
            MouseHoveringOver = this;
            StopCoroutine("OnMouseExit");
            StopCoroutine("OnMouseDown");
            while (time < 1)
            {
                SlotAnimateAt(time += Time.smoothDeltaTime * 3f);
                yield return new WaitForEndOfFrame();
            }

            // Start flickering
            Vector3 tempScale = transform.localScale;
            float tempTime = 0.05f;
            while (true)
            {
                if (tempTime > 0.9f)
                {
                    tempTime = 0;
                }
                tempTime += Time.smoothDeltaTime;

                curveEvalution = TileMouseOverCurve.Evaluate(tempTime) * 20f;
                transform.localScale = tempScale + Vector3.right * curveEvalution + Vector3.up * curveEvalution;
                if(SlotContent != null)
                    SlotContent.transform.Rotate(0,Time.smoothDeltaTime * 200f, 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator OnMouseExit()
    {
        MouseHoveringOver = null;
        if (SlotContent != null)
            slotContent.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        if (ShopItemsHookedTo != this)
        {
            StopCoroutine("OnMouseEnter");
            StopCoroutine("OnMouseDown");
            while (time > 0)
            {
                SlotAnimateAt(time -= Time.smoothDeltaTime * 2f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator OnMouseDown()
    {
        StopCoroutine("OnMouseEnter");
        ShopItemsHookedTo = this;
        ShopItems.gameObject.SetActiveRecursively(true);
        shopInstance.ResetShopItems();
        Vector3 newPosition = transform.localPosition;
        newPosition.z = -12f;
        ShopItems.localPosition = newPosition;
        if (SlotContent != null)
            slotContent.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));

        Vector3 tempPosition = initialPosition;
        Vector3 tempScale = initialScale;
        slotColor.a = 0.3f;
        SlotAnimateAt(1f);
        while (true)
        {
            if (ShopItemsHookedTo != this)
            {
                StartCoroutine(OnMouseExit());
                break;
            }
            transform.localScale = Vector3.Lerp(transform.localScale, tempScale + Vector3.right * 2.5f + Vector3.up * 2.5f, Time.smoothDeltaTime * 10f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, tempPosition + Vector3.back * 6f, Time.smoothDeltaTime * 10f);
            yield return new WaitForEndOfFrame();
        }
    }
    // end GUI

    GameObject slotContent = null;
    internal GameObject SlotContent
    {
        set
        {
            slotContent = value;
            if (slotContent != null)
            {
                slotContent.transform.parent = transform;
                slotContent.SetActiveRecursively(Shop.isVisible);
                SetLayerRecursively(slotContent, 4);
                slotContent.transform.localPosition = new Vector3(0, 0, 0.1f);
                slotContent.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                slotContent.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                Destroy(slotContent.GetComponent<TowerAttack>());
            }
        }
        get
        {
            return slotContent;
        }
    }
    GameObject actualTower = null;
    internal GameObject ActualTower
    {
        set
        {
            actualTower = value;
        }
        get
        {
            return actualTower;
        }
    }
    internal void DeleteTower()
    {
        Destroy(actualTower);
        Destroy(slotContent);
    }
    void SetLayerRecursively(GameObject obj, int newLayer)
    {

        if (null == obj)
        {

            return;

        }



        obj.layer = newLayer;



        foreach (Transform child in obj.transform)
        {

            if (null == child)
            {

                continue;

            }

            SetLayerRecursively(child.gameObject, newLayer);

        }

    }
}
