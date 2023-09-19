using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public delegate void PlayerWinHandler();
    public static event PlayerWinHandler OnPlayerWin;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("PLAYER"))
        {
            Debug.Log("dfasf");
            PlayerWin();
        }
    }

    void PlayerWin()
    {
        OnPlayerWin();
    }

}
