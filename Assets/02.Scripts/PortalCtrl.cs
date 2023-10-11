using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalCtrl : MonoBehaviour
{
    public int sceneNum;
    public List<GameObject> buttons;
    public GameObject effect;
    private bool isOpen = false;
    //int buttonCnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push()
    {
        foreach(GameObject button in buttons)
        {
            if (button.GetComponent<PushButtonCtrl>()?.isPush == false)
                return;
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
                Debug.Log("You Win!");
                SceneManager.LoadScene(sceneNum + 1);
            }
        }
   
    }
}
