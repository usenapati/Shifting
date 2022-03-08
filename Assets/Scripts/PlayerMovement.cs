using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody player;
    PlayerControls controls;
    CapsuleCollider collider;
    public float speed;
    public float jumpForce = 300f;
    Vector2 dir;
    float sprintMod = 1f;
    float halfPlayerHeight;
    float camHeight;
    bool crouched = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        halfPlayerHeight = collider.bounds.extents.y;
        camHeight = Camera.main.transform.localPosition.y;
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
        controls.Movement.Crouch.performed += Crouch;
        controls.Movement.Crouch.Enable();
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
        if (Grounded() && !crouched)
        {
            player.isKinematic = false;
            player.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
    private void Crouch(CallbackContext ctx)
    {
        if (!crouched)
        {
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, 0, Camera.main.transform.localPosition.z);
            collider.height = collider.height / 2;
            collider.center = new Vector3(collider.center.x, -collider.height/2, collider.center.z);
            crouched = true;
            sprintMod /= 2;
        }
        else
        {
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, camHeight, Camera.main.transform.localPosition.z);
            collider.height = collider.height * 2;
            collider.center = new Vector3(collider.center.x, 0, collider.center.z);
            crouched = false;
            sprintMod *= 2;
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
        return Physics.Raycast(transform.position, -Vector3.up, halfPlayerHeight + .1f);
    }
}
