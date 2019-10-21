using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SchoolBus : MonoBehaviour
{
    public AudioSource CarArriving;
    public int RandomNumber;
    private Rigidbody NYUBus;
    public bool IsComing, IsLosing, StartRandom,Success, Arrived, OnTheWay5min;
    public float Timer;
    
    public GameObject panel, SchoolBusGO;
    public TMPro.TMP_Text NoteText;
    public GameObject Door;
    public float RandomTimer;
    public bool TapID;
    public int LoadEndingMissingBus;
    public Animation PanelAni;
    
    

    void Start()
    {
        NYUBus = GetComponent<Rigidbody>();
        panel.SetActive(true);
        
        NoteText.text = "";
        StartRandom = true;
        SchoolBusGO.transform.position = new Vector3(15.3f,-11.2f,119.7f);
    }
    
    


    void Update()
    {
        
        
      
        Ray ray = new Ray(Door.transform.position, transform.forward);

        float rayDist = 8f;
        
        Debug.DrawRay(ray.origin ,ray.direction *rayDist, Color.yellow);
        
        RaycastHit DoorRaycastHit = new RaycastHit();
        
        if (Physics.Raycast(ray, out DoorRaycastHit, rayDist))
        {
            if (DoorRaycastHit.transform.gameObject.name == "BusStop")
            {
                if(!CarArriving.isPlaying)
                    CarArriving.Play();
                
                IsComing = false;            
                StartCoroutine(timer(20f));
                Arrived = true;
                Door.transform.position -= new Vector3(0f, 0f, 2f);
                
            } 

        }
        if (Input.GetMouseButtonDown(0) && TapID)
        {
            PlayerController.instance.TapYourID();
        }

        
        
        //Random a chance for school bus to come
        if (StartRandom && PlayerController.instance.TimerStart)
        {
            //StartCoroutine(timer2(5f));
           RandomTimer += Time.deltaTime;
           if (RandomTimer >=10)
           {
               RandomNumber = Random.Range(0, 5);
               RandomTimer = 0;
           }
        }

        
        if (RandomNumber == 1)
        {
            StartCoroutine(timer2(10f));
            
            OnTheWay5min = true;
            RandomNumber = 0;
            StartRandom = false;
        }
        
        
        //check if losing
        if (IsLosing)
        {
            LoadEndingMissingBus +=1;
        }

        if (LoadEndingMissingBus == 1 && !PlayerController.instance.NotMissing)
        {
            Debug.Log("ending: You Miss Today's Shuttle Bus");
            //PlayerController.instance.DisableButtonAndText();
            PlayerController.instance.button1.gameObject.SetActive(false);
            PlayerController.instance.button2.gameObject.SetActive(false);
            PlayerController.instance.EndingPanel.gameObject.SetActive(true);
            PlayerController.instance.EndingPanelAni.Play();
            PlayerController.instance.EndingText.gameObject.SetActive(true);
            PlayerController.instance.EndingTextAni.Play();
            PlayerController.instance.EndingText.text = "Ending 1: Too Slow\n" +
                                                        "\nUnfortunately,\nYou Miss Today's Only Shuttle Bus.\n" +
                                                        "Time table? \n" +
                                                        "Those are fake.\n" +
                                                        "\n Press R to Play Again.";
            PlayerController.instance.RestartBool = true;
            
        }
    }

    public void OnTheBus()
    {
        TapID = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
        {
            //Time.timeScale = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (IsComing)
        {
            NYUBus.AddForce(0, 0, Random.Range(-10,-40), ForceMode.Impulse);
        }
    }
    
    IEnumerator timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
       
        IsComing = true;
        if (Success == false)
        {
            IsLosing = true;
        }
       
    }
    IEnumerator timer2(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IsComing = true;
        SchoolBusGO.transform.position = new Vector3(15.3f,-0.01f,119.7f);
    }
}

  


    
    
    


