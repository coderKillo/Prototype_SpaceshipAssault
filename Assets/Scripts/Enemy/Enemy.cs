using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private int scorePoints = 3;
    [SerializeField] private int HealthPoints = 1;

    private Renderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    void Update()
    {

    }

    public void Hit()
    {
        StartCoroutine(HitAnimation());

        HealthPoints--;
        if (HealthPoints <= 0)
        {
            Dead();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Hit();
    }

    private IEnumerator HitAnimation()
    {
        var color = m_renderer.material.color;
        m_renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        m_renderer.material.color = color;
    }

    private void Dead()
    {
        GameManager.instance.ObstacleDestroyed(scorePoints);
        Destroy(gameObject);
        var exposionObject = GameObject.Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(exposionObject, 3f);
    }
}
