using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private int scorePoints = 3;
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
        GameManager.instance.ObstacleDestroyed(scorePoints);
        Destroy(gameObject);
        var exposionObject = GameObject.Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(exposionObject, 3f);
    }
}
