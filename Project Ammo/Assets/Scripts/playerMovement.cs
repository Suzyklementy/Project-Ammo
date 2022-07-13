using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float playerSpeed = 3f;

    public float playerRunSpeed = 7f;

    public float jumpStrenght = 5f;

    public float playerSpeedInJump = 2f;

    float yRotate = 0f;

    float forward;

    float sideway;

    float jump;

    public bool runInAir = false;

    public bool preesS = false;

    CharacterController cc;

    private void Start()
    {

        cc = GetComponent <CharacterController>();

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {

        //rotate in x

        float xRoatate = Input.GetAxis("Mouse X");

        transform.Rotate(0, xRoatate, 0);

        //rotate in y

        yRotate -= Input.GetAxis("Mouse Y");

        yRotate = Mathf.Clamp(yRotate, -90, 80);

        Camera.main.transform.localRotation = Quaternion.Euler(yRotate, 0, 0);

        //walk

        if (cc.isGrounded)
        {

            forward = Input.GetAxis("Vertical") * playerSpeed;

            sideway = Input.GetAxis("Horizontal") * playerSpeed;

            runInAir = false;

        }
        else if(cc.isGrounded == false && runInAir == true)
        {

            forward = Input.GetAxis("Vertical") * playerRunSpeed - 3;

        }
        else 
        {

            forward = Input.GetAxis("Vertical") * playerSpeedInJump;

            sideway = Input.GetAxis("Horizontal") * playerSpeedInJump;

        }

        preesS = false;

        //Runing

        if (Input.GetKey(KeyCode.S))
        {

            preesS = true;

        }

        if (Input.GetKey(KeyCode.LeftShift) && cc.isGrounded && preesS == false)
        {

            forward = Input.GetAxis("Vertical") * playerRunSpeed;

            runInAir = true;

        }
        
        //jump

        jump += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButton("Jump") && cc.isGrounded)
        {

            jump = jumpStrenght;

        }

        Vector3 playerMovement = new Vector3(sideway, jump, forward);

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);

    }

}
