using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{


    [SerializeField] private float jumpSpeed;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject gameController;
    private Vector2 MoveVector;
    private CharacterController _char;
    public int numberOfStars;
    private bool restart;
    private bool startMove;
    



    void Start()
    {
        _char = GetComponent<CharacterController>();
        startMove = false;
    }


    void Update()
    {
        Jump();
        PseudoAnimation();
    }
    void FixedUpdate()
    {
        if (restart == true)
        {

            transform.position = new Vector3(-1.65f, 0, 0);
            restart = false;
        }
       

    }

    void Jump()
    {
        if (startMove == true)
        {
           

            if (Input.GetMouseButtonDown(0) && tag == "Player")
            {
                verticalVelocity = jumpSpeed;
            }
            verticalVelocity -= gravity * Time.deltaTime;
            MoveVector = new Vector2(0, verticalVelocity);
            _char.Move(MoveVector * Time.deltaTime);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            this.tag = "Death";
        }

        if (other.tag == "Star" || other.tag == "Сollected")
        {
            gameController.GetComponent<GameController>().CounterStars();
        }
    }




    void PseudoAnimation()
    {
        if (verticalVelocity > 0)
        {

            transform.rotation = Quaternion.Euler(0, 0, 45);


        }

        if (verticalVelocity < -0.04)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);

        }
    }

    public void StartPosition()
    {
        restart = true;
        startMove = true;
        verticalVelocity = 0;
    }

}
