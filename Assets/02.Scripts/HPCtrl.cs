using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCtrl : MonoBehaviour
{
    public static int hp = 3;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    // Start is called before the first frame update
    void Start()
    {
        life1.GetComponent<Image>().enabled = true;
        life2.GetComponent<Image>().enabled = true;
        life3.GetComponent<Image>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        switch (hp)
        {
            case 0:
                life1.GetComponent <Image>().enabled = false;
                break;
            case 1:
                life2.GetComponent <Image>().enabled = false;
                break;
            case 2:
                life3.GetComponent <Image>().enabled = false;
                break;
        }
    
        
    }
}