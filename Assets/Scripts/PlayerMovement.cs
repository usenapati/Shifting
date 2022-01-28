using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody player;
    PlayerControls controls;
    public float speed;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = Vector2.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GetComponent<Rigidbody>();
        controls = new PlayerControls();
        controls.Movement.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        controls.Movement.Movement.canceled += ctx => dir = Vector2.zero;
        controls.Movement.Movement.Enable();
    }

    void FixedUpdate()
    {
        dir.Normalize();
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        player.velocity = transform.forward * dir[1] * speed - transform.right * dir[0] * speed;
    }
}
