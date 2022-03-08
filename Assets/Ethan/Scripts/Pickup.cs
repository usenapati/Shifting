using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Transform TheDest;

    void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = TheDest.position;
        this.transform.parent = GameObject.Find("destination").transform;
    }

    void OnMouseUp()
    {
        GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }
}
