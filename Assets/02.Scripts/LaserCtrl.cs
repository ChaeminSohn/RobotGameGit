using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCtrl : MonoBehaviour
{
    public float damage;
    public float force = 5000.0f;
    private Rigidbody rb;
    private Transform tr;

    public GameObject sparkEffect;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        tr.Rotate(0, -90, 90);
        rb.AddForce(-transform.up * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (!coll.collider.CompareTag("DRONE"))
        {
            if (coll.collider.CompareTag("PLAYER"))
                coll.collider.transform.GetComponent<PlayerCtrl>()?.OnDamage();
            ContactPoint cp = coll.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            Destroy(this.gameObject);
            Destroy(spark, 0.5f);
        }
    }

}
