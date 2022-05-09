using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerMovementVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] boostFlames;
    [SerializeField] private TrailRenderer[] trails;
    [SerializeField] private ParticleSystem warpTunnel;
    [SerializeField] private Volume volume;

    private LensDistortion m_lensDistortion;
    private ChromaticAberration m_chromaticAberration;
    private bool m_isMoving = false;

    void Start()
    {
        volume.profile.TryGet<LensDistortion>(out m_lensDistortion);
        volume.profile.TryGet<ChromaticAberration>(out m_chromaticAberration);
    }

    void Update()
    {
        if (GameManager.instance.isPlayerMoving() && !m_isMoving)
        {
            m_isMoving = true;
            startMoving();
        }
        else if (!GameManager.instance.isPlayerMoving() && m_isMoving)
        {
            m_isMoving = false;
            stopMoving();
        }
    }

    void startMoving()
    {
        StartCoroutine(distortion(1.5f, -0.7f));
        StartCoroutine(aberration(1.5f, 1));

        foreach (var boost in boostFlames)
        {
            boost.Play();
        }
        foreach (var trail in trails)
        {
            trail.emitting = true;
        }
    }

    void stopMoving()
    {
        StartCoroutine(distortion(1, 0));
        StartCoroutine(aberration(1, 0));
        foreach (var boost in boostFlames)
        {
            boost.Stop();
        }
        foreach (var trail in trails)
        {
            trail.emitting = false;
        }
    }

    IEnumerator distortion(float time, float intensity)
    {
        float progress = 0;

        while (progress < time)
        {
            progress += Time.deltaTime;

            var new_value = Mathf.Lerp(m_lensDistortion.intensity.value, intensity, progress / time);
            m_lensDistortion.intensity.value = new_value;

            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator aberration(float time, float intensity)
    {
        float progress = 0;

        while (progress < time)
        {
            progress += Time.deltaTime;

            var new_value = Mathf.Lerp(m_chromaticAberration.intensity.value, intensity, progress / time);
            m_chromaticAberration.intensity.value = new_value;

            yield return new WaitForSeconds(0);
        }
    }
}
