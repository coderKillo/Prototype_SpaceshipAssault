using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private GameObject explosionFX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
        var exposionObject = GameObject.Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(exposionObject, 3f);
    }
}
