using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class SchoolBus : MonoBehaviour
{
    public int RandomNumber;
    private Rigidbody NYUBus;
    public bool IsComing, IsLosing, StartRandom,Success, Arrived, OnTheWay5min,SC;
    public float Timer;
    
    public GameObject panel;
    public TMPro.TMP_Text NoteText;
    public GameObject Door;
    public float RandomTimer;
    

    void Start()
    {
        NYUBus = GetComponent<Rigidbody>();
        panel.SetActive(true);
        NoteText.text = "You have 30 minutes to get to Washington Square Park Campus from Metrotech!";
        StartRandom = true;
    }
    
    


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NoteText.text = "";
            panel.SetActive(false);
        }
      
        Ray ray = new Ray(Door.transform.position, transform.forward);

        float rayDist = 8f;
        
        Debug.DrawRay(ray.origin ,ray.direction *rayDist, Color.yellow);
        
        RaycastHit DoorRaycastHit = new RaycastHit();
        
        if (Physics.Raycast(ray, out DoorRaycastHit, rayDist))
        {
            if (DoorRaycastHit.transform.gameObject.name == "BusStop")
            {
                IsComing = false;            
                StartCoroutine(timer(20f));
                Arrived = true;
                
            } else if (DoorRaycastHit.transform.gameObject.name == "Player"& Arrived)
            {
                Door.transform.Translate(0f,0f,2f);
                Success = true;
                IsLosing = false;
                panel.SetActive(true);                                      
                NoteText.text = "You successfully catch the shuttle bus!";  
                
            }
        }
        
        //Random a chance for school bus to come
        if (StartRandom)
        {
           // StartCoroutine(timer2(5f));
           RandomTimer += Time.deltaTime;
           if (RandomTimer >=2)
           {
               RandomNumber = Random.Range(0, 10);
               RandomTimer = 0;
           }
        }

        
        if (RandomNumber == 1)
        {
            StartCoroutine(timer2(10f));
            //IsComing = true;
            OnTheWay5min = true;
            RandomNumber = 0;
            StartRandom = false;
        }

        

        

        //check if losing
        if (IsLosing)
        {
            Timer += Time.deltaTime;
            if (Timer >= 2f)
            {
                Debug.Log("You Miss Today's Shuttle Bus");
                panel.SetActive(true);
                NoteText.text = "You Miss Today's Shuttle Bus!";
                Time.timeScale = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsComing)
        {
            NYUBus.AddForce(0, 0, -1, ForceMode.Impulse);
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
    }
}

  


    
    
    


