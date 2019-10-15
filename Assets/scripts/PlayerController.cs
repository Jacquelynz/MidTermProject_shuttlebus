using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private Rigidbody PlayerRB;

    private Vector3 myInput;

    public bool grounded;
    
    public TMPro.TMP_Text SofaText;

    public Button button1;

    public Button button2;

    public Text but1Text, but2Text;

    public Image Schedules;
    public Image RTT, RedDot;

     // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PlayerRB = GetComponent<Rigidbody>();
        SofaText.text = "";
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        Schedules.gameObject.SetActive(false);
        RTT.gameObject.SetActive(false);
        RedDot.gameObject.SetActive(false);
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
        
        //disable Schedules & RTT
        if (Input.GetKey(KeyCode.Escape))
        {
            Schedules.gameObject.SetActive(false);
            RTT.gameObject.SetActive(false);
            Button1.instance.SitOnSofa = false;
            RedDot.gameObject.SetActive(false);
        }
        
        //raycast to detect if player hit sofa
        /*Ray playerRay = new Ray(transform.position, transform.forward);
        float playerRaydist = 2f;
        Debug.DrawRay(playerRay.origin, playerRay.direction * playerRaydist, Color.green);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(playerRay, out hit, playerRaydist))
        {
            if (hit.transform.gameObject.name == "RedSofa")
            {
                button1.gameObject.SetActive(true);
                button2.gameObject.SetActive(true);
                SofaText.text = "Do you want to sit on this sofa?";
            }
        }*/

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "RedSofa" && !Button1.instance.SitOnSofa)
        {
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            but1Text.text = "Yes";
            but2Text.text = "No!";
            SofaText.text = "Do you want to sit on this sofa?";
            GameObject.Find("Button1").GetComponent<Button1>().SitOnSofa = false;
        }
    }

    public void DisableButtonAndText()
    {
        
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        SofaText.text = "";
    }

    public void ReadTimeTableOrSchedules()
    {
        SofaText.text = "Which one do you want to read? Real time map or Schedules?";
        but1Text.text = "Real Time Map";
        but2Text.text = "Schedules";
        //red dot appears
        if (GameObject.Find("bus2").GetComponent<SchoolBus>().OnTheWay5min)
        {
            print("red dot");
            RedDot.gameObject.SetActive(true);
        }
    }

    public void LoadRTT(float TimeToCome)
    {
        RTT.gameObject.SetActive(true);
        DisableButtonAndText();
    }

    public void LoadSd()
    {
        Debug.Log("ShowSchedules");
        Schedules.gameObject.SetActive(true);
        DisableButtonAndText();

    }

    void FixedUpdate()
    {
        myInput.y = PlayerRB.velocity.y/5;
        PlayerRB.velocity = myInput * 5f;
        
    }
}
