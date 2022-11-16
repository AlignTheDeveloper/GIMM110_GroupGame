using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Camera cam;


    [Header("Movement Variables")]
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    public float moveSpeed;

    
    [Header("Staff Variables")]
    public StaffController staff;

    
    [Header("Controller support")]
    public bool useController;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = (moveInput * moveSpeed);

        //Rotate with Mouse
        if(!useController)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            // if the ray from the camera hits something, the ray length is set
            if(groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 lookHere = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, lookHere, Color.green);

                transform.LookAt(new Vector3(lookHere.x, transform.position.y, lookHere.z));
            }
        }

        //Rotate with Controller
        if(useController)
        {
            Vector3 playerDirection; 
            playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }

        }

        //Shoot
        if(Input.GetButtonDown("Shoot"))
            {
                staff.isFiring = true;
            }
            if(Input.GetButtonUp("Shoot"))
            {
                staff.isFiring = false;
            }
    }

    // Fixed Update is used for physics
    private void FixedUpdate() 
    {
        rb.velocity = moveVelocity;
    }
}
