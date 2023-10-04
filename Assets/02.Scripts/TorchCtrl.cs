using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCtrl : MonoBehaviour
{
    public bool playAura = false;
    public List<ParticleSystem> particleObjects = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (ParticleSystem particle in particleObjects)
            particle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter  (Collider other)
    {
        if (other.CompareTag("FIRE"))
        {
            playAura = true;
            foreach (ParticleSystem particle in particleObjects)
                particle.gameObject.SetActive(true);
        }
    }
}
