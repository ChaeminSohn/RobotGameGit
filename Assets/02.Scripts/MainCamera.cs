using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform targetTr;
    public Vector3 offset;
    private Transform camTr;
    public float damping = 10.0f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        camTr = GetComponent<Transform>();  
    }

    void LateUpdate()
    {
        /*Vector3 pos = targetTr.position + offset;
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);
        transform.LookAt(targetTr.forward);*/
    }
}
