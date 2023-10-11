using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    bool running;
    bool jumping;
    public bool isBorder;
    public float moveSpeed = 15.0f;
    public float jumpPower = 15.0f;
    public float XturnSpeed = 100.0f;
    public float YturnSpeed = 100.0f;
    private float eulerAngleX;
    private float eulerAngleY;
    private float turnLimitX = -80;
    private float turnLimitY = 50;
    private float commandRange = 2.0f;
    private readonly float initHp = 100.0f;
    public float currHP;

    private GameManager manager;

    private Transform tr;
    public Transform headTr;
    private Rigidbody rb;
    private RaycastHit slopehit;
    private Camera playerCamera;
    private RaycastHit hit;
    public GameObject image_Fire;




    Dictionary<KeyCode, Action> keyDictionary;

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = gameObject.GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

    }
    IEnumerator Start()
    {
        /*keyDictionary = new Dictionary<KeyCode, Action>
        {
            {KeyCode.Alpha1, keyDown_1 },
            {KeyCode.Alpha2, keyDown_2 },
            {KeyCode.Alpha3, keyDown_3 },
          
        };*/
        tr = GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        manager = GameManager.instance;
        FinishPoint.OnPlayerWin += this.OnPlayerWin;
 
        currHP = initHp;
        XturnSpeed = 0.0f;
        YturnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        XturnSpeed = 100.0f;
        YturnSpeed = 100.0f;
        Debug.Log("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
        move();
        turn();
        //jump();

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * commandRange * 50, Color.green);


        if (Input.anyKeyDown)
        {
            /*foreach(var dic in keyDictionary){
                if (Input.GetKeyDown(dic.Key))
                    dic.Value();
            }*/
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,
                    out hit, commandRange * 50, 1 << 9))
                {
                    Debug.Log("Start Ctrl" + hit.transform.name);
                    GameManager.instance.SetCtrl(hit.transform.gameObject);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                manager.GiveCommand("Move");
            }
            else if (Input.GetButton("Follow"))
                manager.GiveCommand("Follow");
            /*else if (Input.GetButton("Move"))
                manager.GiveCommand("Move");*/
            else if (Input.GetButton("RobotSkill"))
                manager.GiveCommand("Skill");



        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        FreezeRotation();
        //TiltOnSlope();
        StopToWall();
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, commandRange * 50, LayerMask.GetMask("SpecialObject")))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "FIRE":
                    image_Fire.gameObject.SetActive(true);
                    break;


            }
        }
        else if(image_Fire.gameObject  != null)
            image_Fire.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
            anim.SetBool("isJump", jumping);
        }

    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        //Debug.DrawRay(transform.position + 3 * Vector3.up, transform.forward * 10, Color.green);
        //Debug.DrawRay(transform.position, -transform.up * 5, Color.blue);
        isBorder = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward ,commandRange, LayerMask.GetMask("Wall"));
    }

    void TiltOnSlope()
    {

        var angle = Vector3.Angle(transform.up, slopehit.normal);
        Quaternion rot = Quaternion.LookRotation(slopehit.normal);
        Vector3 up = rot.eulerAngles;
        tr.forward = up;

    }

    void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);   
        Vector3 getVel = new Vector3(h, 0, v) * moveSpeed;
        Vector3 locVel = transform.InverseTransformDirection(getVel);
        locVel.x = h;
        locVel.y = 0;
        locVel.z = v;
        locVel *= moveSpeed;

        running = Input.GetButton("Run");

        /*if(!jumping)
         moveVector = new Vector3(hAxis, 0, vAxis).normalized;
        else
         moveVector = new Vector3(hAxis, 0, vAxis).normalized/2;
        */
        if (!isBorder || v <= 0)
        {
            if (running && !jumping)
                //transform.position += moveVector * 2 * moveSpeed * Time.deltaTime;
                //rb.velocity = getVel * 2;
            //rb.velocity = transform.TransformDirection(locVel) * 2;
            tr.Translate(moveDir.normalized *
             2 * moveSpeed * Time.deltaTime);
            else
            {
                //transform.position += moveVector * moveSpeed * Time.deltaTime;
               tr.Translate(moveDir.normalized
                * moveSpeed * Time.deltaTime);
                //rb.velocity = transform.TransformDirection(locVel);
               //rb.velocity = getVel;
            }

        }

        anim.SetBool("isWalk", h == 0 && v == 0);
        anim.SetBool("isRun", running && !jumping);


        //Ray ray = new Ray(transform.position, -transform.up);
        //if (Physics.Raycast(ray, out slopehit, 1.0f, 1 << 7))
            //TiltOnSlope();

        //transform.LookAt(transform.position + moveVector);
    }

    void turn()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        eulerAngleY += mouseX * XturnSpeed;
        eulerAngleX -= mouseY * YturnSpeed;
        mouseX = ClampAngle(mouseX, turnLimitX, turnLimitY);
        //eulerAngleX = ClampAngle(eulerAngleX, turnLimitX, turnLimitY);
        //transform.rotation = Quaternion.Euler(0, eulerAngleY, 0);
        // headTr.rotation = Quaternion.Euler(eulerAngleX, 0, 0);
        tr.Rotate(Vector3.up * mouseX * XturnSpeed * Time.deltaTime);
        playerCamera.transform.Rotate(Vector3.left * mouseY * YturnSpeed * Time.deltaTime);

        //tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * );
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumping = true;
            anim.SetBool("isJump", jumping);
        }
    }

    public void OnDamage()
    {
        HPCtrl.hp -= 1;
        tr.Translate(Vector3.back * Time.deltaTime * 300, Space.Self);
        currHP -= 40.0f;
        if (currHP < 0)
            OnPlayerDie();
    }
    void OnPlayerWin()
    {
        Debug.Log("You Win!");
    }

    void OnPlayerDie()
    {
        Debug.Log("You Died");
    }
    /*void keyDown_1()
    {
        GameManager.instance.SetCtrl(1);
    }
    void keyDown_2()
    {
        GameManager.instance.SetCtrl(2);
    }
    void keyDown_3()
    {
        GameManager.instance.SetCtrl(3);
    }*/
}


