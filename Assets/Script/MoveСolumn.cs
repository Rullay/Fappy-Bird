using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveСolumn : MonoBehaviour
{
    public float const_speed;
    public float speed;
    public float columnSpacing;
    [SerializeField] private GameObject topColumn;
    [SerializeField] private GameObject bottomСolumn;
    public bool point;





    void Start()
    {
        speed = const_speed;
        point = true;
        if (columnSpacing != 0)
        {
            columnSpacing = columnSpacing * 0.5f;
        }

        topColumn.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + columnSpacing, transform.position.z);
        bottomСolumn.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y - columnSpacing, transform.position.z);
    }


    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);

    }

    

}

