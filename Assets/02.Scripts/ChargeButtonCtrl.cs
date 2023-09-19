using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeButtonCtrl : MonoBehaviour
{
    public RobotCtrl.Type type;
    public GameObject pairObject;
    private bool isActive;
    public GameObject image_Type;
    public Vector3 offset = new Vector3(0, 8.0f, 0);
    // Start is called before the first frame update
    void Start()
    {
        isActive = pairObject.gameObject.activeSelf;
        image_Type.transform.position = (this.gameObject.transform.position + offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCharge(RobotCtrl.Type type)
    {
        Debug.Log("start charge");
        if(this.type == type && pairObject.gameObject.activeSelf == isActive)
        {
            pairObject.gameObject.SetActive(!isActive);
        }
    }
    public void StopCharge()
    {
        if (pairObject.activeSelf)
            pairObject.gameObject.SetActive(isActive);
    }
}
