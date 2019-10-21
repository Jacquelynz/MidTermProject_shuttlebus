using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
public class NormalBusController : MonoBehaviour
{
    public GameObject NormalBus;
    
    public float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Timer += Time.deltaTime;
        if (Timer >= 60f && !GameObject.Find("bus2").GetComponent<SchoolBus>().OnTheWay5min)
        {
            Instantiate(NormalBus);
            Timer = 0f;
        }
    }
}
