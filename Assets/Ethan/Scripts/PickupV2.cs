using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupV2 : MonoBehaviour
{
    private GameObject heldObj;

    public GameObject player;
    public float pickUpRange = 5;
    public Transform holdParent;
    public float moveForce = 20;

    private PlayerControls controls;
    private InputAction pickupControls;


    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Pickup.PickupDrop.performed += pickUp;
        controls.Pickup.PickupDrop.Enable();
    }

    private void OnDisable()
    {
        controls.Pickup.PickupDrop.Disable();
    }

    

    private void pickUp(InputAction.CallbackContext obj)
    {
        //Debug.Log("pick up!");
        if (heldObj == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
              {
                if (hit.transform.gameObject.CompareTag("moveable")) {
                    Rigidbody pickupObjRig = hit.transform.gameObject.GetComponent<Rigidbody>();
                    pickupObjRig.transform.position = holdParent.transform.position;
                    pickupObjRig.useGravity = false;
                    pickupObjRig.drag = 10;
                    pickupObjRig.transform.parent = holdParent;
                    heldObj = pickupObjRig.transform.gameObject;
                }
              }
        }
        else
        {
            Rigidbody pickedUpObj = heldObj.GetComponent<Rigidbody>();
            pickedUpObj.useGravity = true;
            pickedUpObj.drag = 1;

            heldObj.transform.parent = null;
            heldObj = null;
        }
    }

    private void Update()
    {
        if (heldObj != null)
        {
            heldObj.transform.position = holdParent.transform.position;
            heldObj.transform.rotation = player.transform.rotation;
            //moveObject();
        }
    }

    private void moveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position = heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }
}
