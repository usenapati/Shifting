using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairTrigger : Interactable
{
    public GameObject door;
    public GameObject openedDoor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(interactName) )
        {
            //Debug.Log("Entered");
            door.SetActive(false);
            openedDoor.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        door.SetActive(true);
        openedDoor.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
