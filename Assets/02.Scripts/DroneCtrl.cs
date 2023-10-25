using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DroneCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE, SCOUT, DETECT, FIRE, TURN
    }

    public State state = State.IDLE;
    public float scoutDist = 5.0f;
    public float attackDist = 5.0f;
    public float moveSpeed = 1.0f;
    public bool isRunning = true;
    public bool isDetect = false;
    public float detectTime;
    public GameObject scanner;
    public Transform firePos;
    private Transform playerTr;
    private Transform droneTr;

    Quaternion currentRotation;
    Vector3 targetEulerAngle;
    Quaternion targetRotation;

    float lerpTime = 1.0f;
    float currentTime = 0;
    float turnDegree = 180.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        droneTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        state = State.SCOUT;

        StartCoroutine(DroneAction());
    }
    IEnumerator CheckDroneState()
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(0.3f);

            float distance = Vector3.Distance(playerTr.position, droneTr.position);
        }
    }
    IEnumerator DroneAction()
    {
        while (isRunning) {

            switch (state)
            {
                case State.IDLE:
                    break;
                case State.SCOUT:
                    droneTr.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    break;
                case State.DETECT:
                    Debug.DrawRay(firePos.position, (playerTr.position - firePos.position), Color.red);
                    //droneTr.LookAt(playerTr.position);
                    break;
                case State.FIRE:
                    transform.GetComponent<FireCtrl>()?.Fire();
                    detectTime -= 2.0f;
                    state = State.DETECT;
                    break;
                case State.TURN:
                    
                    currentRotation = droneTr.rotation;
                    targetEulerAngle = droneTr.rotation.eulerAngles;
                    targetEulerAngle.y += turnDegree;
                    targetRotation = Quaternion.Euler(targetEulerAngle);
                    scanner.gameObject.SetActive(false);
                    while (currentTime < lerpTime)
                    {
                        this.transform.rotation = Quaternion.Euler(Vector3.Lerp(
                            currentRotation.eulerAngles, targetRotation.eulerAngles, currentTime / lerpTime));
                        currentTime += Time.deltaTime;
                        yield return null;
                    }
                    scanner.gameObject.SetActive(true);
                    currentTime = 0;
                    state = State.SCOUT;
                    break;

            }
            yield return new WaitForSeconds(0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "DRONE_STOP_POINT_180":
                isRunning = false;
                turnDegree = 180.0f;
                state = State.TURN;
                isRunning = true;
                break;
            case "DRONE_STOP_POINT_90":
                isRunning = false;
                turnDegree = 90.0f;
                state = State.TURN;
                isRunning = true;
                break;
            case "WALL":
                isRunning = false;
                state = State.TURN;
                isRunning = true;
                break;
            case "PLAYER":
                isDetect = true;
                state = State.DETECT;
                detectTime = 0.0f;
                break;
            case "DRONE":
                isRunning = false;
                turnDegree = 180.0f;
                state = State.TURN;
                isRunning = true;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "PLAYER":
                detectTime += 0.1f;
                if (detectTime >= 3.0f)
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
