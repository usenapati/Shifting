using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Interactable
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == interactName)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
