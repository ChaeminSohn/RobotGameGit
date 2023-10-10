using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> robots = new List<GameObject>();
    public List<GameObject> ctrlRobots = new List<GameObject>();    
    public static GameManager instance = null;
    private RaycastHit hit;
    GameObject player;
    Camera cam;
    private RectTransform robotPanel;
 
    private Vector2 rectSize_robot;
   
    private float width;
    public GameObject robot;
    public GameObject locationImage;
    public GameObject Player;
    private GameObject moveSpot;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        
    }
    void Start()
    {
        //GameObject robotGroup = GameObject.Find("ROBOTS");
        
        foreach(GameObject robot in GameObject.FindGameObjectsWithTag("ROBOT"))
            robots.Add(robot);
   
        //robot = GameObject.Find("MiniRobot");
        //ctrl.Add(0);
        
        cam = Camera.main;
        Player = GameObject.FindWithTag("PLAYER");
        robotPanel = GameObject.FindGameObjectWithTag("ROBOT_CNT")?.GetComponent<Image>().GetComponent<RectTransform>();
        rectSize_robot = robotPanel.sizeDelta;
     

    }

    public void GiveCommand(System.String cmd)
    { 
        if(cmd == "Move" && hit.collider.tag == "Ground")
        {
            if (moveSpot != null)
                Destroy(moveSpot);
            moveSpot = Instantiate(locationImage, hit.point, Quaternion.identity);
            moveSpot.transform.LookAt(Player.transform.position);
            Destroy(moveSpot, 2.0f);
        }
        foreach(GameObject robot in ctrlRobots)
        {
            robot.GetComponent<RobotCtrl>()?.Command(cmd, hit.point);
        }
    }
   

    public void SetCtrl(GameObject robot)
    {
        if (!ctrlRobots.Contains(robot))
        {
            ctrlRobots.Add(robot);
            rectSize_robot += new Vector2(225.0f, 0);
            robotPanel.sizeDelta = rectSize_robot;
            Debug.Log("Start Control" + robot.name);
        }
        else
        {
            ctrlRobots.Remove(robot);
            rectSize_robot -= new Vector2(215.0f, 0);
            robotPanel.sizeDelta = rectSize_robot;
            //robot.GetComponent<RobotCtrl>()?.Command("Wait", Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
    }

 
    public void OnPlayerWin()
    {

    }
}
