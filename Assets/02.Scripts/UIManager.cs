using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    Image fireImage;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {

    }
    public void showFireImage()
    {
        if ( fireImage != null ) { 
                fireImage.gameObject.SetActive(true);
        }
    }

    public void noImage()
    {
        if (fireImage.gameObject.activeSelf)    
        {
            fireImage.gameObject.SetActive(false);
        }
    }
}
