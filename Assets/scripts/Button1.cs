using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Button1 : MonoBehaviour
{
    public static Button1 instance;
    public GameObject player;
    public bool SitOnSofa;
    
    
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void button1()
    {
        if (SitOnSofa)
        {
                 Debug.Log("ShowRTT");
                 PlayerController.instance.LoadRTT(0); 
        }
        if (SitOnSofa == false)
        {
           
            Debug.Log("Yes I want to sit on this sofa");
            player.transform.position = new Vector3(-15.07f,4.8f,-19.92f);
            player.transform.eulerAngles = new Vector3(0f,268f,0f);
            PlayerController.instance.ReadTimeTableOrSchedules();
            SitOnSofa = true;
            GameObject.Find("Button2").GetComponent<Button1>().SitOnSofa = true;
        }

        
        
        
    }
    
    public void button2()
    {
        if (SitOnSofa == false)
        {
            PlayerController.instance.DisableButtonAndText();
            SitOnSofa = false;
        }

        if (SitOnSofa)
        {
            Debug.Log("ShowSchedules");
            PlayerController.instance.LoadSd();
        }
        
    }
}
