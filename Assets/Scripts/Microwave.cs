using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Fork"))
        {
            boss.GetComponent<EventMovement>().BeginPatrol();
        }
    }
}
