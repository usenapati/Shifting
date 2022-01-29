using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals(interactName))
        {
            transform.localPosition = relative.transform.localPosition;
            transform.rotation = relative.transform.rotation;
            GameObject.Destroy(collision.gameObject);
            pickup.heldObj = null;
        }
    }
}
