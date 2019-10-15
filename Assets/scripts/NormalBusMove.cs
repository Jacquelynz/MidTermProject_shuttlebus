using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBusMove : MonoBehaviour
{
    public GameObject Door;
    public Rigidbody Bus;
    public bool isComing,Arrived;
    public TMPro.TMP_Text NoteText;
    // Start is called before the first frame update
    void Start()
    {
        isComing = true;
        
        NoteText = GameObject.Find("Canvas/NormalBusText").GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isComing)
        {
            Bus.AddForce(0, 0, -1, ForceMode.Impulse);
        }
        
        Ray ray = new Ray(Door.transform.position, transform.forward);

        float rayDist = 4f;
        
        Debug.DrawRay(ray.origin ,ray.direction *rayDist, Color.yellow);
        
        RaycastHit DoorRaycastHit = new RaycastHit();
        

        if (Physics.Raycast(ray, out DoorRaycastHit, rayDist))
        {
            if (DoorRaycastHit.transform.gameObject.name == "BusStop")
            {
                isComing = false;
                Arrived = true;
                
            } else if (DoorRaycastHit.transform.gameObject.name == "Player"& Arrived)
            {
                Door.transform.Translate(0f,0f,2f);
                NoteText.text = "You entered the fake bus haha";
                Time.timeScale = 0f;

            }
        }
    }
}
