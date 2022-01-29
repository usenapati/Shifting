using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : Interactable
{
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(interactName))
        {
            boss.GetComponent<EventMovement>().BeginPatrol();
            pickup.heldObj = null;
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(relative.gameObject);
        }
    }
}
