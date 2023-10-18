using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject fire_image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFireImage()
    {
        if ( fire_image != null ) {
            if (fire_image.gameObject.activeSelf)
            {
                fire_image.SetActive(false);
            }
            else
            {
                fire_image.SetActive(true);
            }
        }
    }
}
