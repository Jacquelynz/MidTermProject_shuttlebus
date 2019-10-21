using System.Collections;
using System.Collections.Generic;
using System.Timers;
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
        if (SitOnSofa && !GameObject.Find("bus2").GetComponent<SchoolBus>().TapID && !GameObject.Find("Player")
                .GetComponent<PlayerController>().HeadSubway)
        {
            Debug.Log("ShowRTT");
            PlayerController.instance.LoadRTT(0); 
        }
        if (SitOnSofa == false && !GameObject.Find("bus2").GetComponent<SchoolBus>().TapID && !GameObject.Find("Player")
                                                                        .GetComponent<PlayerController>().HeadSubway)
        {
            Debug.Log("Yes I want to sit on this sofa");
            player.transform.position = new Vector3(-15.07f,4.8f,-19.92f);
            player.transform.eulerAngles = new Vector3(0f,268f,0f);
            PlayerController.instance.ReadTimeTableOrSchedules();
            SitOnSofa = true;
            GameObject.Find("Button2").GetComponent<Button1>().SitOnSofa = true;
        }

        if (GameObject.Find("bus2").GetComponent<SchoolBus>().TapID)
        {
            PlayerController.instance.panelAni2.Play("paneldown");
            PlayerController.instance.FailToFindID();
        }

        if (PlayerController.instance.HeadSubway)
        {
            Debug.Log("Ending: Out of Money and return home by subway");
            PlayerController.instance.button1.gameObject.SetActive(false);
            PlayerController.instance.button2.gameObject.SetActive(false);
            PlayerController.instance.EndingPanel.gameObject.SetActive(true);
            PlayerController.instance.EndingPanelAni.Play();
            PlayerController.instance.EndingTextAni.Play();
            PlayerController.instance.EndingText.text = "Ending 8: Unhappy Subway\n" +
                                                        "\nYou paid all your money to get a subway ticket.\n" +
                                                        "However, you don't feel happy after returning home.\n" +
                                                        "\n(Press R to Play Again)";
            PlayerController.instance.DisableButtonAndText();
            PlayerController.instance. NotMissing = true;
            PlayerController.instance.RestartBool = true;
        }

    }
    
    public void button2()
    {
        if (SitOnSofa == false && !PlayerController.instance.HeadSubway)
        {
            PlayerController.instance.panelAni2.Play("paneldown");
            PlayerController.instance.DisableButtonAndText();
            SitOnSofa = false;
            PlayerController.instance.PlayerMove = true;
            Debug.Log("leave sofa");
        }

        if (SitOnSofa && !GameObject.Find("bus2").GetComponent<SchoolBus>().TapID && !GameObject.Find("Player")
                .GetComponent<PlayerController>().HeadSubway)
        {
            PlayerController.instance.panelAni2.Play("paneldown");
            Debug.Log("ShowSchedules");
            PlayerController.instance.LoadSd();
        }
        
        if (GameObject.Find("bus2").GetComponent<SchoolBus>().TapID)
        {
            PlayerController.instance.panelAni2.Play("paneldown");
            PlayerController.instance.SuccessToFindID();
        }
        
        if (PlayerController.instance.HeadSubway)
        {
            PlayerController.instance.panelAni2.Play("panelup");
            PlayerController.instance.textAni.Play("textup");
            PlayerController.instance.SofaText.text = "I should think about this later.\n" +
                                                      "(Esc to close)";
            PlayerController.instance.button1.gameObject.SetActive(false);
            PlayerController.instance.button2.gameObject.SetActive(false);
        }
        
    }
}
