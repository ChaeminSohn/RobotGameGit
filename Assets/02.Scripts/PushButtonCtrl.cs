using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.UI;
using UnityEngine;

public class PushButtonCtrl : MonoBehaviour
{
    public RobotCtrl.Type type;
    public GameObject pairObject;
    private Transform field;
    private bool isActive;
    bool isPushed;
    // Start is called before the first frame update
    void Start()
    {
        isActive = pairObject.gameObject.activeSelf;    
        field = pairObject.transform.Find("ForceField");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ROBOT") || other.CompareTag("PLAYER"))
        {
            isPushed = true;
            if (type == RobotCtrl.Type.ALL || other.gameObject.GetComponent<RobotCtrl>()?.type == type)
            {
                if (pairObject.CompareTag("PORTAL"))
                {
                    pairObject.GetComponent<PortalCtrl>()?.Triggered();
                }

                else
                {
                    if (isActive)
                    {
                        pairObject.gameObject.SetActive(false);
                        pairObject.GetComponent<Collider>().enabled = false;
                    }
                    else
                    {
                        pairObject.gameObject.SetActive(true);
                        pairObject.GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ROBOT") || other.CompareTag("PLAYER"))
        {
            isPushed = false;
            if (isActive)
            {
                pairObject.gameObject.SetActive(true);
                pairObject.GetComponent<Collider>().enabled = true;
            }
            else
            {
                pairObject.gameObject.SetActive(false);
                pairObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public bool isPush()
    {
        return isPushed;
    }
}
