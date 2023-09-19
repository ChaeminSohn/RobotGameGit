using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCtrl : MonoBehaviour
{
    public List<GameObject> buttons;
    //int buttonCnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    private void Open()
    {
        Debug.Log("Portal Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You Win!");
    }
}
