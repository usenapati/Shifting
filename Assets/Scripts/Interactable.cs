using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public abstract class Interactable : MonoBehaviour
{
    PlayerControls controls;
    public bool effectOther;
    public GameObject relative;
    public string interactName;
    public PickupV2 pickup;
    protected Vector3 dist;
    private void Start()
    {
        if (relative != null)
        {
            dist = relative.transform.position - transform.position;
        }
        if (effectOther)
        {
            controls = new PlayerControls();
            controls.TimeShift.Timeshift.performed += TimeShift;
            controls.TimeShift.Timeshift.Enable();
        }
    }
    private void OnDestroy()
    {
        if (controls != null)
            controls.Disable();
        if (effectOther)
        {
            GameObject.Destroy(relative);
        }
    }
    private void TimeShift(CallbackContext ctx)
    {
        if (relative != null)
        {
            relative.transform.position = transform.position + dist;
            relative.transform.eulerAngles = transform.eulerAngles;
        }
    }
}
