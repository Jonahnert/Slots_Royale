using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSystemAutoDestruct : MonoBehaviour {

    private ParticleSystem pSystem;
    private void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(pSystem)
        {
            if (!pSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("This object does not have a particle system component!");
        }
    }
}
