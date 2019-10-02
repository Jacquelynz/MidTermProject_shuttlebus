using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooks : MonoBehaviour
{
    private float verticalAngle = 0f;
   

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            transform.parent.Rotate(0f,mouseX*2f,0f);
            verticalAngle -= mouseY*2f;
            verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);
            
            transform.localEulerAngles = new Vector3(verticalAngle,transform.localEulerAngles.y,0f);
        }
    }
}
