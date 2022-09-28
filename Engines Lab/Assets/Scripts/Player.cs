using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerAction inputAction;
    Vector2 movement;
    float moveSpeed = 10f;

    public Camera cam;

    Rigidbody rb;
    bool grounded;
    float jump = 10f;
    Animator animator;

    public GameObject bullet;
    public Transform bulletPos;

    private void OnEnable()
    {
        inputAction = new PlayerAction();

        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputAction.Player.Move.performed += cntxt => movement = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => movement = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();
        inputAction.Player.Shoot.performed += cntxt => Shoot();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * movement.y * Time.deltaTime * moveSpeed, Space.Self);
        transform.Translate(Vector3.right * movement.x * Time.deltaTime * moveSpeed, Space.Self);

        Vector3 offset = new Vector3(0, 1, 0);

        if(Physics.Raycast(transform.position + offset, Vector3.down, 1f))
        {
            //Debug.Log("Hit");
            grounded = true;
        }
        else
        {
            //Debug.Log("Miss");
            grounded = false;
        }
    }

    private void Jump()
    {
        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            grounded = false;
        }
    }

    private void Shoot()
    {
        //Debug.Log("Shoot");
        
        Rigidbody brb = Instantiate(bullet, bulletPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        brb.AddForce(Vector3.forward * 1000f);
    }
}
