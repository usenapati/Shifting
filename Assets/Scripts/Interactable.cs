using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool effectOther;
    public GameObject relative;
    public string interactName;
    public PickupV2 pickup;
    private void FixedUpdate()
    {
        
    }
    private void OnDestroy()
    {
        if (effectOther)
        {
            GameObject.Destroy(relative);
        }
    }
}
