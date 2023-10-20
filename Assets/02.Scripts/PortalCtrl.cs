using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalCtrl : MonoBehaviour
{
    public List<GameObject> triggers;
    public GameObject effect;
    private bool isOpen = false;
    //int buttonCnt = 0;
    
    void Start()
    {
        effect.SetActive(false);
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
