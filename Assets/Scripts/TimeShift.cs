using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShift : MonoBehaviour
{
    // Level Origin
    public Transform origin;
    // Track X Distance
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
        }
    }

    void TimeTravel()
    {
        // Switch Time Bool
        isInPast = !isInPast;
        Debug.Log("Past: " + isInPast + " Future: " + !isInPast);
        // Set Player's position
        // Console Log which period player is in
    }
}
