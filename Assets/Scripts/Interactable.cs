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
    private void Start()
    {
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
            relative.transform.localPosition = transform.localPosition;
            relative.transform.localEulerAngles = transform.localEulerAngles;
        }
    }
}
