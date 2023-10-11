using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotCntCtrl : MonoBehaviour
{
    public GameObject robot1;
    public GameObject robot2;
    public GameObject robot3;
    public GameObject robot4;
    // Start is called before the first frame update
    void Start()
    {
        robot1.GetComponent<Image>().enabled = false;
        robot2.GetComponent<Image>().enabled = false;
        robot3.GetComponent<Image>().enabled = false;
        robot4.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.instance.ctrlRobots.Count)
        {
            case 0:
                robot1.GetComponent<Image>().enabled = false;
                break;
            case 1:
                robot1.GetComponent<Image>().enabled = true;
                robot2.GetComponent<Image>().enabled = false;
                break;
            case 2:
                robot2.GetComponent<Image>().enabled = true;
                robot3.GetComponent<Image>().enabled = false;
                break;
            case 3:
                robot3.GetComponent<Image>().enabled = true;
                robot4.GetComponent<Image>().enabled = false;
                break;
            case 4:
                robot4.GetComponent<Image>().enabled = true;
                break;

        }
    }
}
