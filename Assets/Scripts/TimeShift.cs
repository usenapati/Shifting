using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShift : MonoBehaviour
{
    // Level Origin
    public Transform origin;
    // Track X Distance
    public float xDistance;
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

    void TimeTravel()
    {
        // Switch Time Bool
        isInPast = !isInPast;
        
        // Set Player's position
        if (isInPast)
        {
            transform.position = transform.position + new Vector3(-xDistance, 0, 0);
        }
        else
        {
            transform.position = transform.position + new Vector3(xDistance, 0, 0);
        }
        
        // Console Log which period player is in
        Debug.Log("Past: " + isInPast + " Future: " + !isInPast);
    }
}
