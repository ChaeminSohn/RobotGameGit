using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButtonCtrl : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("ROBOT"))
            door.gameObject.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        door.gameObject.SetActive(true);
    }
}
