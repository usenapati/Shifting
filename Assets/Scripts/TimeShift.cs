using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        isInPast = false;
        // Get Access to Player Controls
        controls = new PlayerControls();
        controls.TimeShift.Timeshift.performed += ctx => timeTravelled = ctx.performed;
        controls.TimeShift.Timeshift.Enable();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if button is pressed
        if (timeTravelled)
        {
            Debug.Log("TIME TRAVELLED");
            TimeTravel();
            timeTravelled = false;
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

    void TimeTravel()
    {
        // Switch Time Bool
        isInPast = !isInPast;
        Vector3 dir = (future.transform.position - origin.transform.position).normalized;
        dir.y = 0;
        // Set Player's position
        if (isInPast)
        {
            transform.position = transform.position + dir * -distance;
        }
        else
        {
            transform.position = transform.position + dir * distance;
        }

        // Console Log which period player is in
        Debug.Log("Past: " + isInPast + " Future: " + !isInPast);
    }
}
