using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    PlayerControls controls;
    public float sensitivity = 100f;
    float yMax = 75;
    float yMin = -75;
    float horiz;
    float vert;
    Vector2 rotate;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controls = new PlayerControls();
        controls.Movement.Sight.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Movement.Sight.canceled += ctx => rotate = Vector2.zero;
        controls.Movement.Sight.Enable();
        horiz = transform.eulerAngles.y;
        vert = transform.eulerAngles.x;
        Debug.Log(vert + " " + horiz);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeSinceLevelLoad > 1)
        {
            horiz += rotate.x * sensitivity * Time.deltaTime;
            vert -= rotate.y * sensitivity * Time.deltaTime;
            vert = Mathf.Clamp(vert, yMin, yMax);
        }
        transform.eulerAngles = new Vector3(vert, horiz, 0);
    }
}
