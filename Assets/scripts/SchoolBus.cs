using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class SchoolBus : MonoBehaviour
{
    public int RandomNumber;
    private Rigidbody NYUBus;
    public bool IsComing, IsLosing, StartRandom,Success, Arrived;
    public float Timer;
    //public Collider Door;
    public GameObject panel;
    public TMPro.TMP_Text NoteText;
    public GameObject Door;

    void Start()
    {
        NYUBus = GetComponent<Rigidbody>();
        panel.SetActive(false);
        NoteText.text = "";
        StartRandom = true;
    }


    void Update()
    {
        Ray ray = new Ray(Door.transform.position, -transform.right);

        float rayDist = 4f;
        
        Debug.DrawRay(ray.origin ,ray.direction *rayDist, Color.yellow);
        
        RaycastHit DoorRaycastHit = new RaycastHit();
        

        if (Physics.Raycast(ray, out DoorRaycastHit, rayDist))
        {
            if (DoorRaycastHit.transform.gameObject.name == "BusStop")
            {
                IsComing = false;            
                StartCoroutine(timer());
                Arrived = true;
            } else if (DoorRaycastHit.transform.gameObject.name == "Player"& Arrived)
            {
                Success = true;                                             
                panel.SetActive(true);                                      
                NoteText.text = "You successfully catch the shuttle bus!";  
            }
        }
        
        //Random a chance for school bus to come
        if (StartRandom)
        {
            RandomNumber = Random.Range(0, 10);
        }

        
        if (RandomNumber == 1)
        {
            IsComing = true;
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
    
    IEnumerator timer()
    {
        yield return new WaitForSeconds(5f);
        
        IsComing = true;
        if (Success == false)
        {
            IsLosing = true;
        }
       
    }
}

    /*    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "BusStop")
        {
            IsComing = false;
            StartCoroutine(timer());
            
            Timer += Time.deltaTime;
            if (Timer >= 5f)
            {
                Door.isTrigger = true;
                IsComing = true;
                Timer = 0f;
                IsLosing = true;
            } 
        }

        if (other.gameObject.name == "Player")
        {
            Success = true;
            panel.SetActive(true);
            NoteText.text = "You successfully catch the shuttle bus!";
        }
    }*/  


    
    
    


