using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRB;

    private Vector3 myInput;

    private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 jump = new Vector3(0f, 2f, 0f);
    

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward;

        
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRB.AddForce(jump, ForceMode.Impulse );
        } 
        
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            PlayerRB.velocity = myInput * 5f;
        }
        
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }
}
