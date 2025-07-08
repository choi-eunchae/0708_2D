using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject pinObject;
    private Pin currPin;

    void Start()
    {
        PreparePin();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currPin != null && GameManager.instance.isGameOver == false)
        {
            currPin.Launch();
            currPin = null;
            Invoke("PreparePin", 0.1f);
        }
    }

    void PreparePin()
    {
        GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
        currPin = pin.GetComponent<Pin>();
    }
}
