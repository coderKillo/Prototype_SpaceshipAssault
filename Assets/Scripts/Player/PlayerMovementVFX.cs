using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] boostFlames;
    [SerializeField] private ParticleSystem warpTunnel;

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.isPlayerMoving())
        {
            foreach (var boost in boostFlames)
            {
                boost.Play();
            }
        }
        else
        {
            foreach (var boost in boostFlames)
            {
                boost.Stop();
            }
        }
    }
}
