using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE, SCOUT, DETECT, FIRE
    }

    public State state = State.IDLE;
    public float scoutDist = 5.0f;
    public float attackDist = 5.0f;
    public float moveSpeed = 1.0f;
    public bool isRunning = true;
    public bool isDetect = false;
    public float detectTime;

    public Transform firePos;
    private Transform playerTr;
    private Transform droneTr;
    
    // Start is called before the first frame update
    void Start()
    {
        droneTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        state = State.SCOUT;

        //StartCoroutine(DroneAction());
    }

    void Update()
    {
        if (isRunning) { 
            switch (state)
            {
                case State.IDLE:
                    break;
                case State.SCOUT:
                    droneTr.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    break;
                case State.DETECT:
                    Debug.DrawRay(firePos.position, (playerTr.position - firePos.position), Color.red);
                    break;
                case State.FIRE:
                    transform.GetComponent<FireCtrl>()?.Fire();
                    detectTime -= 2.0f;
                    state = State.DETECT;
                    break;

            }
        }
    }

    void LateUpdate()
    {
        if (isRunning)
        {

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "DRONE_STOP_POINT":
                isRunning = false;
                droneTr.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                isRunning = true;
                break;
            case "PLAYER":
                isDetect = true;
                state = State.DETECT;
                detectTime = 0.0f;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "PLAYER":
                detectTime += 0.1f;
                if (detectTime >= 10.0f)
                    state = State.FIRE;
                break;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "PLAYER":
                isDetect = false;
                state = State.SCOUT;
                break;

        }
    }
}
