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
    Color colorTrans;
    Color colorClear;
    // Start is called before the first frame update
    void Start()
    {
        life1.GetComponent<Image>().enabled = true;
        life2.GetComponent<Image>().enabled = true;
        life3.GetComponent<Image>().enabled = true;
        colorClear = life1.GetComponent<Image>().color;
        colorClear.a = 1.0f;
        colorTrans = life1.GetComponent<Image>().color;
        colorTrans.a = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        switch (hp)
        {
            case 0:
                life1.GetComponent<Image>().color = colorTrans;
                //life1.GetComponent <Image>().enabled = false;
                break;
            case 1:
                life2.GetComponent<Image>().color = colorTrans;
                //life2.GetComponent <Image>().enabled = false;
                break;
            case 2:
                life3.GetComponent<Image>().color = colorTrans;
                //life3.GetComponent <Image>().enabled = false;
                break;
            case 3:
                life1.GetComponent<Image>().color = colorClear;
                life2.GetComponent<Image>().color = colorClear;
                life3.GetComponent<Image>().color = colorClear;
                break;
                
        }
    }

    public void hpFull()
    {
        hp = 3;
        life1.GetComponent<Image>().color = colorClear;
        life2.GetComponent<Image>().color = colorClear;
        life3.GetComponent<Image>().color = colorClear;
    }
}
