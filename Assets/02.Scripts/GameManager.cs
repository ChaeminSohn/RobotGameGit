using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> robots = new List<GameObject>();
    public List<GameObject> ctrlRobots = new List<GameObject>();    
    public static GameManager instance = null;
    private RaycastHit hit;
    private int sceneNum = 0;
    Camera cam;

   

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
        cam = Camera.main;
        Player = GameObject.FindWithTag("PLAYER");
        DontDestroyOnLoad(this.Player);
        //DontDestroyOnLoad (this.cam);


    }
    void Start()
    {
        //GameObject robotGroup = GameObject.Find("ROBOTS");
        
        foreach(GameObject robot in GameObject.FindGameObjectsWithTag("ROBOT"))
            robots.Add(robot);
   
        //robot = GameObject.Find("MiniRobot");
        //ctrl.Add(0);
        
       

     

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

            Debug.Log("Start Control" + robot.name);
        }
        else
        {
            ctrlRobots.Remove(robot);

            //robot.GetComponent<RobotCtrl>()?.Command("Wait", Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
    }

    public void ChangeScene()
    { 
        SceneManager.LoadScene(++sceneNum);
        robots = new List<GameObject>();
        ctrlRobots = new List<GameObject>();
        HPCtrl.hp = 3;
        foreach (GameObject robot in GameObject.FindGameObjectsWithTag("ROBOT"))
            robots.Add(robot);
    
    }
    public void OnPlayerWin()
    {

    }
}
