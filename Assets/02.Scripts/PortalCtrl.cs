using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PortalCtrl : MonoBehaviour
{
    public List<GameObject> triggers;
    public GameObject effect;
    private bool isOpen = false;
    public AudioClip portalSfx;
    private new AudioSource audio;
    //int buttonCnt = 0;

    void Start()
    {
        effect.SetActive(false);
        audio = GetComponent<AudioSource>();    
    }


    public void Triggered()
    {
        foreach(GameObject trigger in triggers)
        {
            if (trigger.CompareTag("BUTTON"))
            {
                if (trigger.GetComponent<PushButtonCtrl>()?.isPush() == false)
                    return;
            }
            else if (trigger.CompareTag("FIRE"))
            {
                if (trigger.GetComponent<TorchCtrl>()?.isLit() == false)
                    return;
            }
           
        }
        Open();
    }
    public void Open()
    {
        effect.SetActive (true);
        audio.PlayOneShot(portalSfx,1.0f);
        isOpen = true;
      
    }


    private void OnTriggerStay(Collider other)
    {
        if (isOpen)
        {
            if (other.gameObject.CompareTag("PLAYER"))
            {
                SceneManager.LoadScene("LoadingScene");
                //GameManager.instance.ChangeScene();
            }
        }
   
    }
}
