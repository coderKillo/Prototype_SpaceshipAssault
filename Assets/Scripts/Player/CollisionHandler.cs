using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionVFX;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        explosionVFX.gameObject.transform.position = this.transform.position;
        explosionVFX.Play();
        GameManager.instance.PlayerHit();
    }
}
