using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeShift : MonoBehaviour
{
    //future empty object
    public GameObject future;
    // Level Origin
    public Transform origin;
    // Track X Distance
    public float distance;
    // Player Movement Object
    PlayerMovement playerMovement;
    // Player Controls
    PlayerControls controls;
    // Boolean for TimePeriod
    public bool isInPast;
    // Boolean for TimeShift Press
    bool timeTravelled;
    // Box Volume
    public Volume timeshiftVolume;
    public LensDistortion LD;
    float changeval = 1f;
    float minVal = -1f;
    float maxVal = 1f;
    bool finishedStartEffect = false;
    Vector3 dir;


    // Start is called before the first frame update
    void Start()
    {
        isInPast = true;
        // Get Access to Player Controls
        controls = new PlayerControls();
        controls.TimeShift.Timeshift.performed += TimeTravel;
        controls.TimeShift.Timeshift.Enable();

        LensDistortion tmp;

        if (timeshiftVolume.profile.TryGet(out tmp))
        {
            LD = tmp;
        }
        //LD.active = false;
    }

    private void FixedUpdate()
    {
        if (!isInPast)
        {
            if (timeTravelled && LD.intensity.value >= minVal)
            {
                LD.intensity.value -= changeval * Time.deltaTime;
                Debug.Log("To Future - Lens Distortion Intensity: " + LD.intensity.value);
            }
            if (timeTravelled && (LD.intensity.value <= minVal))
            {

                transform.position = transform.position + dir * -distance;
                LD.intensity.value = 0;
                LD.intensity.Override(0);
                timeTravelled = false;
            }
        }
        else
        {
            if (timeTravelled && LD.intensity.value <= maxVal)
            {
                LD.intensity.value += changeval * Time.deltaTime;
                Debug.Log("To Past - Lens Distortion Intensity: " + LD.intensity.value);
            }
            if (timeTravelled && (LD.intensity.value >= maxVal))
            {

                transform.position = transform.position + dir * distance;
                LD.intensity.value = 0;
                LD.intensity.Override(0);
                timeTravelled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AntiTimeShiftZone")
        {
            Debug.Log("Entered Anti TimeShift Zone");
            this.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AntiTimeShiftZone")
        {
            Debug.Log("Exited Anti TimeShift Zone");
            timeTravelled = false;
            this.enabled = true;
        }
    }

    public void TimeTravel(CallbackContext ctx)
    {
        // Switch Time Bool
        isInPast = !isInPast;
        dir = (future.transform.position - origin.transform.position).normalized;
        dir.y = 0;
        timeTravelled = true;
        finishedStartEffect = false;

        // Console Log which period player is in
        Debug.Log("Past: " + isInPast + " Future: " + !isInPast);        
    }

}
