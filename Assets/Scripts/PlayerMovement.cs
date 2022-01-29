using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody player;
    PlayerControls controls;
    public float speed;
    public float jumpForce = 300f;
    Vector2 dir;
    float sprintMod = 1f;
    float playerHeight;
    // Start is called before the first frame update
    void Start()
    {
        playerHeight = GetComponent<CapsuleCollider>().bounds.extents.y;
        dir = Vector2.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GetComponent<Rigidbody>();
        controls = new PlayerControls();
        controls.Movement.Movement.performed += Movement;
        controls.Movement.Movement.canceled += Movement;
        controls.Movement.Movement.Enable();
        controls.Movement.Sprint.performed += ctx => sprintMod = 2f;
        controls.Movement.Sprint.canceled += ctx => sprintMod = 1f;
        controls.Movement.Sprint.Enable();
        controls.Movement.Jump.performed += Jump;
        controls.Movement.Jump.Enable();
    }
    private void Movement(CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
        if (dir == Vector2.zero)
        {
            if (Grounded())
                player.isKinematic = true;
        } 
        else
        {
            player.isKinematic = false;
        }
    }
    private void Jump(CallbackContext ctx)
    {
        if (Grounded())
        {
            player.isKinematic = false;
            player.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
    void FixedUpdate()
    {
        dir.Normalize();
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        player.velocity = new Vector3(0,player.velocity.y,0) + transform.forward * dir[1] * speed * sprintMod - transform.right * dir[0] * speed * sprintMod;
    }
    bool Grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, playerHeight + .1f);
    }
}
