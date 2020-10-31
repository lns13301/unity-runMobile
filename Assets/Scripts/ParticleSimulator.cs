using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSimulator : MonoBehaviour
{
    private float lastInterval;
    private ParticleSystem pSystem;
    private float deltaTime; 
 
    void Awake()
    {
        lastInterval = Time.realtimeSinceStartup;
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        deltaTime = Time.realtimeSinceStartup - lastInterval;

        pSystem.Simulate(deltaTime * 2, true, false);
        lastInterval = Time.realtimeSinceStartup;
    }
}
