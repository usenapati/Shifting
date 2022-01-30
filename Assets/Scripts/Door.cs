using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Vector3 rotation;
    public Vector3 position;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals(interactName))
        {
            transform.localPosition = position;
            transform.localEulerAngles = rotation;
            GameObject.Destroy(collision.gameObject);
            pickup.heldObj = null;
        }
    }
}
