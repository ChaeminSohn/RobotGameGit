using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class TorchCtrl : MonoBehaviour
{
    public bool playAura = false;
    public List<ParticleSystem> particleObjects = new List<ParticleSystem>();
    public GameObject pairObject;

    bool onFire = false;

    void Start()
    {
        foreach (ParticleSystem particle in particleObjects)
            particle.gameObject.SetActive(false);
    }



    private void OnTriggerEnter  (Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            Invoke("Fire", 1.0f);
        }
    }

    void Fire()
    {
        playAura = true;
        foreach (ParticleSystem particle in particleObjects)
            particle.gameObject.SetActive(true);
        onFire = true;
        pairObject.GetComponent<PortalCtrl>()?.Triggered();
    }

    public bool isLit()
    {
        return onFire;
    }
}
