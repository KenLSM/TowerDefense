using UnityEngine;
using System.Collections;

public class Camscript : MonoBehaviour
{
    float LastX;
    float LastY;

    bool EnableY = true;
    // Use this for initialization
    void Start()
    {
        LastX = Input.mousePosition.x;
        LastY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.mousePosition.x - LastX;
        this.gameObject.transform.Rotate(Vector3.up * deltaX);
        LastX = Input.mousePosition.x;

        if (EnableY)
        {
            float deltaY = Input.mousePosition.y - LastY;
            this.gameObject.transform.Rotate(Vector3.right * deltaY);
            LastY = Input.mousePosition.y;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableY = !EnableY;
            LastY = Input.mousePosition.y;
            this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.rotation.eulerAngles.y, 0);
        }
    }
}