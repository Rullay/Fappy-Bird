using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    public float speed;
   
   

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World); 
        if(transform.position.x < - 5)
        {
            this.tag = "Сollected";
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            this.tag = "Сollected";  
        }
    }
}
