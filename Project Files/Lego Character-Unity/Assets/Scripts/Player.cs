using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;

    public float gravity = 20.0f;
    public float jumpForce = 10.0f;
    public float speed = 50.0f;
    public float turnSpeed = 50.0f;

    public GameObject runFaceVar;
    public GameObject jumpFaceVar;
    public GameObject idleFaceVar;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();

        runFaceVar = GameObject.Find("/Player_Idle/Armature/Root/Spine1/Spine2/Neck/Head/GEOFaceRun");
        jumpFaceVar = GameObject.Find("/Player_Idle/Armature/Root/Spine1/Spine2/Neck/Head/GEOFaceJump");
        idleFaceVar = GameObject.Find("/Player_Idle/Armature/Root/Spine1/Spine2/Neck/Head/GEOFaceIdle");

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && Input.GetKey("up"))
        {
            anim.SetInteger("AnimPar", 1);
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            runFaceVar.SetActive(true);
            idleFaceVar.SetActive(false);
            jumpFaceVar.SetActive(false);

        }
        else if (controller.isGrounded)
        {
            anim.SetInteger("AnimPar", 0);
            moveDirection = transform.forward * Input.GetAxis("Vertical") * 0;
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            runFaceVar.SetActive(false);
            idleFaceVar.SetActive(true);
            jumpFaceVar.SetActive(false);

        }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            anim.SetInteger("AnimPar", 2);
            moveDirection.y = jumpForce;

            runFaceVar.SetActive(false);
            idleFaceVar.SetActive(false);
            jumpFaceVar.SetActive(true);

        }

        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
        
    }
}
