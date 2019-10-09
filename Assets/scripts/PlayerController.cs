using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRB;

    private Vector3 myInput;

    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //raycast to detect if player is grounded
        Ray ray = new Ray(transform.position, Vector3.down);

        float rayDist = 1.2f;

        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.green);
        
        //RaycastHit groundhit = new RaycastHit();

        if (Physics.Raycast(ray, rayDist))
        {
            grounded = true;
            
        } else
        {
            grounded = false;
        }
        
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 jump = new Vector3(0f, 0.5f, 0f);
    

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward;

        
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRB.AddForce(jump, ForceMode.Impulse );
        } 
        
    }

    void FixedUpdate()
    {
        myInput.y = PlayerRB.velocity.y/5;
        PlayerRB.velocity = myInput * 5f;
    }
}
