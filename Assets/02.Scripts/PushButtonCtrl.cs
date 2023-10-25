using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PushButtonCtrl : MonoBehaviour
{
    public RobotCtrl.Type type;
    public GameObject pairObject;
    private Transform field;
    private bool isActive;
    bool isPushed;
    public AudioClip openSfx;
    private new AudioSource audio;
    void Start()
    {
        isActive = pairObject.gameObject.activeSelf;    
        field = pairObject.transform.Find("ForceField");
        audio = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
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
                        audio.PlayOneShot(openSfx, 1.0f);
                    }
                    else
                    {
                        pairObject.gameObject.SetActive(true);
                        pairObject.GetComponent<Collider>().enabled = true;
                        audio.PlayOneShot(openSfx, 1.0f);
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
                audio.PlayOneShot(openSfx, 1.0f);
            }
            else
            {
                pairObject.gameObject.SetActive(false);
                pairObject.GetComponent<Collider>().enabled = false;
                audio.PlayOneShot(openSfx, 1.0f);
            }
        }
    }

    public bool isPush()
    {
        return isPushed;
    }
}
