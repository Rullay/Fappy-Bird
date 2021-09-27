using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFon : MonoBehaviour
{

    [SerializeField] private float speed_const;
    private float speed;
    [SerializeField] private GameObject Fon1;
    [SerializeField] private GameObject Fon2;
   
    void Start()
    {
        speed = speed_const;
    }

   
    void Update()
    {
        transform.Translate(-speed*Time.deltaTime, 0 , 0, Space.World);

        if(Fon2.transform.position.x < 0)
        {
            
            transform.position = transform.position + Fon2.transform.position - Fon1.transform.position;
        }
    }
    
    public void StopMove()
    {
        speed = 0;
    }

    public void StartMove()
    {
        speed = speed_const;
    }

    
}
