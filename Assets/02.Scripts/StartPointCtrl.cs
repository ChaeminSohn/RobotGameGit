using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointCtrl : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("PLAYER");
            player.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
