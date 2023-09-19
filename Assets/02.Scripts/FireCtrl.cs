using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    private Transform playerTr;
    public GameObject laser;
    public Transform firePos;
    RaycastHit hit;
    void Start()
    {
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Debug.DrawRay(firePos.position, (playerTr.position - firePos.position), Color.red);
        Physics.Raycast(firePos.position, playerTr.position - firePos.position, out hit, 2.0f);
        firePos.LookAt(playerTr.position);
        Instantiate(laser, firePos.position, firePos.rotation);
        
    }
}
