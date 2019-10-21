using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource Rain, Dark,Jazz,Sad,VerySad,Crash,Scream;
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
    public Image HealthBar;
    public int InsideMetrotech;
    public TMPro.TMP_Text TimeText,EndingText,Instru,Instru2;
    public int TimeNumber = 300;
    float TimerAll;
    public bool HeadSubway;
    public GameObject EndingPanel,instruPanel;
    public Animation panelAni2,textAni,EndingPanelAni,EndingTextAni;
    public int StartScriptNumber,DoormanNumber,SofaNumber;
    public int SubwayNumber;
    public bool MeetDM;
    public bool PlayerMove;
    public bool TimerStart;
    public bool AddingHealth,LosingHealth;
    public int LoadEndingNoHealth;
    public int LoadEndingFindnoID,LoadEndingFindID;
    public bool FindnoID,FindID;
    public bool NotMissing;
    public bool RestartBool,suicide;
    

     // Start is called before the first frame update
    void Start()
    {
        EndingPanel.gameObject.SetActive(false);
        instance = this;
        PlayerRB = GetComponent<Rigidbody>();
        SofaText.text = "";
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        Schedules.gameObject.SetActive(false);
        RTT.gameObject.SetActive(false);
        RedDot.gameObject.SetActive(false);
        StartScriptNumber = 0;
        panelAni2.Play("panelup");
        textAni.Play("textup");
        LosingHealth = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        //Restart
        if (RestartBool)
        {
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            SofaText.gameObject.SetActive(false);
            Instru2.text = "";
            Instru.text = "";
            instruPanel.gameObject.SetActive(false);
            
            if(!Sad.isPlaying && !suicide)
                Sad.Play();
            
            if(!VerySad.isPlaying && suicide)
                VerySad.Play();
            
            if(Dark.isPlaying)
                Dark.Stop();
            
            if(Jazz.isPlaying)
                Jazz.Stop();
            
            if(Rain.isPlaying)
                Rain.Stop();
            
            if (Input.GetKey(KeyCode.R))
            { 
                Restart();
            }
            
        }
        //a few scripts comes out at the start
        if (StartScriptNumber == 0)
        {
            SofaText.text = "It's' 9:30 PM and your last class is just over. (Click to continue)";
        }

        if (Input.GetMouseButtonDown(0) )
        {
            if (StartScriptNumber < 5)
            {
                textAni.Play("textup");
                panelAni2.Play("panelup");
            }

            if (StartScriptNumber < 7)
            {
                StartScriptNumber += 1;
            }
            if (StartScriptNumber == 6)
            {
                textAni.Play("textdown");
                panelAni2.Play("paneldown");
                SofaText.text = "";
                PlayerMove = true;
                TimerStart = true;
            }

            if (MeetDM)
            {
                DoormanNumber += 1;
            }

            if (DoormanNumber ==1 && MeetDM )
            {
                panelAni2.Play("paneldown");
                textAni.Play("textdown");
                MeetDM = false;
            }

            if (DoormanNumber > 1)
            {
                DoormanNumber = 0;
            }

            
        }

        if (StartScriptNumber == 1)
        {
            SofaText.text = "It's cold outside.";
        }
        if (StartScriptNumber == 2) 
        {
            SofaText.text = "You want to return home.";
        }
        if (StartScriptNumber == 3)
        {
            SofaText.text = "NYU shuttle bus stop is right there outside this building.";
        }
        if (StartScriptNumber == 4)
        {
            SofaText.text = "It's raining so maybe it's not a good choice to go out for long.";
        }
        if (StartScriptNumber == 5)
        {
            SofaText.text = "Waiting for a shuttle bus that may never come or find another way home?";
        }


        if (TimerStart)
        {
            TimerAll += Time.deltaTime;
        }
        
        if (TimerAll > 1)
        {
            TimeNumber--;
            TimerAll = 0f;
        }

        if (TimeNumber == 0)
        {
            Debug.Log("ending: you are out of time");
            EndingPanel.gameObject.SetActive(true);
            EndingPanelAni.Play();
            EndingTextAni.Play();
            EndingText.text = "Ending 2: Unlucky Kid\n" +
                              "\nAfter 5 minutes, \nYou still can't find a way home. \n" +
                              "You are not supposed to stand here forever. \n" +
                              "Sometimes, the shuttle bus never comes.\n" +
                              "\n (Press R to Play Again)";
            NotMissing = true;
            RestartBool = true;

        }
        
        TimeText.text = TimeNumber.ToString();
        if (LoadEndingNoHealth == 1)
        {
            Debug.Log("ending: No Health at all");
            EndingPanel.gameObject.SetActive(true);
            EndingPanelAni.Play();
            EndingTextAni.Play();
            EndingText.text = "Ending 3: Frozen\n" +
                              "\nI've told you. \n" +
                              "It's so cold and raining outside. \n" +
                              "Why are you standing there?\n" +
                              "Waiting for any NYU shuttle bus that never comes?\n" +
                              "\n(Press R to Play Again";
            NotMissing = true;
            RestartBool = true;
        }
        //If not health is losing
        if (InsideMetrotech == 1)
        {
            print("RainSound");
            if(!Rain.isPlaying)
            Rain.Play();
            
            if(!Dark.isPlaying)
                Dark.Play();
            
            if(Jazz.isPlaying)
                Jazz.Stop();
            
            if (LosingHealth)
            {
                HealthBar.transform.localScale -= new Vector3(0.02f*Time.deltaTime,0,0);
            }
            if (HealthBar.transform.localScale.x <= 0f)
            {
                LoadEndingNoHealth += 1;
                
            }
        }

        if (InsideMetrotech == 0)
        {
            if(Rain.isPlaying)
            Rain.Stop();
            
            if(Dark.isPlaying)
                Dark.Stop();
            if(!Jazz.isPlaying)
                Jazz.Play();
            
            if (AddingHealth)
            {
                HealthBar.transform.localScale += new Vector3(0.02f*Time.deltaTime,0,0);
            }
            if (HealthBar.transform.localScale.x >= 1f)
            {
                Debug.Log("Full health");
                AddingHealth = false;
            }
            else
            {
                AddingHealth = true;
            }
        }
        //raycast to detect if player is grounded
        Ray ray = new Ray(transform.position, Vector3.down);

        float rayDist = 2f;

        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.green);
        
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
            SofaText.text = "";
            //Time.timeScale = 1f;
            if (HeadSubway)
            {
                panelAni2.Play("paneldown");
                HeadSubway = false;
                LosingHealth = true;
            }
            PlayerMove = true;
            Instru.text = "";
            Instru2.text = "";
            instruPanel.gameObject.SetActive(false);
        }

        if (FindnoID)
        {
            NotMissing = true;
            LoadEndingFindnoID += 1;
        }

        if (LoadEndingFindnoID == 200)
        {
            EndingPanel.gameObject.SetActive(true);
            EndingPanelAni.Play();
            EndingTextAni.Play();
            EndingText.text = "Ending 6: Not identified\n" +
                              "\nYou ID is obviously not in your backpack.\n" +
                              "You are kicked out of the shuttle bus\n" +
                              "\n(Press R to Play Again)";
            NotMissing = true;
            RestartBool = true;

        }
        if (FindID)
        {
            NotMissing = true;
            LoadEndingFindID += 1;
        }

        if (LoadEndingFindID == 200)
        {
            EndingPanel.gameObject.SetActive(true);
            EndingPanelAni.Play();
            EndingTextAni.Play();
            EndingText.text = "Ending 7: You make it!" +
                              "\n\nCongratulations! \n" +
                              "You successfully catch NYU shuttle bus!\n\n" +
                              "(Press R to Play Again)";
            RestartBool = true;

        }
        

    }

    public void TapYourID()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        panelAni2.Play("panelup");
        textAni.Play("textup");
        SofaText.text = "You should find your ID, where is it?";
        but1Text.text = "My backpack";
        but2Text.text = "My pocket";
    }

    public void FailToFindID()
    {
        panelAni2.Play("panelup");
        SofaText.text = "You didn't find your ID in the backpack";
        textAni.Play("textup");
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        Debug.Log("ending: Fail to find ID and you are kicked out");
        FindnoID = true;
       
        
    }

    public void SuccessToFindID()
    {
        panelAni2.Play("panelup");
        SofaText.text = "Your ID is exactly right there.";
        textAni.Play("textup");
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        GameObject.Find("bus2").GetComponent<SchoolBus>().Success = true;
        GameObject.Find("bus2").GetComponent<SchoolBus>().TapID = false;
        Debug.Log("Success to tap your ID and get on the shuttle bus");
        GameObject.Find("bus2").GetComponent<SchoolBus>().Success = true;
        FindID = true;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "RedSofa")//&& !Button1.instance.SitOnSofa)
        {
            Debug.Log("EnterSofa");
            PlayerMove = false;
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            but1Text.text = "Yes";
            but2Text.text = "No!";
            panelAni2.Play("panelup");
            textAni.Play("textup");
            SofaText.text = "Do you want to sit on this sofa?";
            GameObject.Find("Button1").GetComponent<Button1>().SitOnSofa = false;
            GameObject.Find("Button2").GetComponent<Button1>().SitOnSofa = false;
            
        }

        //if player goes to subway station
        if (other.gameObject.name == "SubwayStation")
        {
            LosingHealth = false;
            PlayerMove = false;
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            but1Text.text = "Yes (Cost $2.75)";
            but2Text.text = "No (Stay here and die alone)";
            panelAni2.Play("panelup");
            textAni.Play("textup");
            SofaText.text = "Do you want to take the Subway home?";
            HeadSubway = true;
        }
        

        if (other.gameObject.tag == "doorman")
        {
            //PlayerMove = false;
            textAni.Play("textup");
            SofaText.text = "Hey, we have no public restrooms!\n" +
                            "(Click to close)";
            panelAni2.Play("panelup");
            MeetDM = true;
        }
    }

    public void DisableButtonAndText()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        SofaText.text = "";
        textAni.Play("textdown");
    }

    public void ReadTimeTableOrSchedules()
    {
        PlayerMove = false;
        panelAni2.Play("panelup");
        textAni.Play("textup");
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
        Instru.text = "Esc to close";
        Instru2.text = "Red dot represents that the shuttle bus is nearby.\n" +
                       "If there's no red dot, the shuttle bus is still far away.\n" +
                       "Esc to close";
        instruPanel.gameObject.SetActive(true);
        PlayerMove = false;
        panelAni2.Play("paneldown");
        RTT.gameObject.SetActive(true);
        DisableButtonAndText();
    }

    public void LoadSd()
    {
        Instru.text = "Esc to close";
        Debug.Log("loadSchedules");
        PlayerMove = false;
        panelAni2.Play("paneldown");
        Debug.Log("ShowSchedules");
        Schedules.gameObject.SetActive(true);
        DisableButtonAndText();

    }

    void FixedUpdate()
    {
        if (PlayerMove)
        {
            myInput.y = PlayerRB.velocity.y/5;
            PlayerRB.velocity = myInput * 5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "6 metrotech")
        {
            InsideMetrotech += 1;
            if (InsideMetrotech > 1)
            {
                InsideMetrotech = 0;
            }
        }

        if (other.gameObject.name == "bus2")
        {
            GameObject.Find("bus2").GetComponent<SchoolBus>().OnTheBus();
            TapYourID();
            LosingHealth = false;
        }

        if (other.gameObject.tag == "normalbus")
        {
            //textAni.Play("textup");
            //panelAni2.Play("panelup");
            //SofaText.text = "You entered the fake bus";
            Debug.Log("ending: You entered the fake bus and not able to return home");
            EndingPanel.gameObject.SetActive(true);
            EndingPanelAni.Play();
            EndingTextAni.Play();
            EndingText.text = "Ending 4: Fake Bus\n" +
                              "\nThis is not the NYU shuttle bus! \n" +
                              "Fake buses won't take you home!\n" +
                              "\n(Press R to Play Again)";
            NotMissing = true;
            RestartBool = true;
            
        }

        if (other.gameObject.name == "TriggerDeathShuttle")
        {
            if (GameObject.Find("bus2").GetComponent<SchoolBus>().IsComing)
            {
                Debug.Log("ending: you die of the car crash");
                CarCrash();
            }
        }
        
        if (other.gameObject.name == "TriggerDeathBus")
        {
            Debug.Log("ending: you die of the car crash");
            CarCrash();
        }
        
        void CarCrash()
        {
            if(!Crash.isPlaying)
                Crash.Play();
            
            if(!Scream.isPlaying)
                Scream.Play();
            
            if(Rain.isPlaying)
                Rain.Stop();
            if(Dark.isPlaying)
                Dark.Stop();
            
            EndingPanel.gameObject.SetActive(true);
            EndingPanelAni.Play();
            EndingTextAni.Play();
            EndingText.text = "Ending 5: Suicide\n" +
                              "\nWhy are you running across the road?\n" +
                              "You were hit by the bus and killed.\n" +
                              "However, maybe this is the final step to escape from all " +
                              "the programming classes you have to take...\n" +
                              "Is it...?\n" +
                              "\n(Press R to Play Again)";
            NotMissing = true;
            RestartBool = true;
            suicide = true;
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
