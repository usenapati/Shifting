using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public int level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            GameManager.instance.sceneLoad = level;
        }
    }
}
