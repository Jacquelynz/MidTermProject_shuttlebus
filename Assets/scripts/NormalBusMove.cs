using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
            Bus.AddForce(0, 0, Random.Range(-10,-40), ForceMode.Impulse);
        }
        
        Ray ray = new Ray(Door.transform.position, transform.forward);

        float rayDist = 8f;
        
        Debug.DrawRay(ray.origin ,ray.direction *rayDist, Color.yellow);
        
        RaycastHit DoorRaycastHit = new RaycastHit();
        

        if (Physics.Raycast(ray, out DoorRaycastHit, rayDist))
        {
            if (DoorRaycastHit.transform.gameObject.name == "BusStop")
            {
                if(!GameObject.Find("bus2").GetComponent<SchoolBus>().CarArriving.isPlaying)
                    GameObject.Find("bus2").GetComponent<SchoolBus>().CarArriving.Play();
                
                isComing = false;
                Arrived = true;
                StartCoroutine(timer());
                Door.transform.Translate(0f,0f,2f);
            } 
        }

        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }

    

    IEnumerator timer()
    {
        yield return new WaitForSeconds(20);
        isComing = true;
    }
}
