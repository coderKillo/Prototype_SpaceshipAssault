using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Boarder")]
    [SerializeField] private float boarderUp = 17.0f;
    [SerializeField] private float boarderDown = -12.0f;
    [SerializeField] private float boarderRight = 20.0f;
    [SerializeField] private float boarderLeft = -20.0f;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 30.0f;

    [SerializeField] private float controlRollFactor = 40.0f;
    [SerializeField] private float controlPitchFactor = 25.0f;
    [SerializeField] private float positionYawFactor = 5.0f;
    [SerializeField] private float positionPitchFactor = 2.0f;
    [SerializeField] private float rotationSpeed = 2.0f;

    [SerializeField] private GameObject model;

    [Header("Shooting")]
    [SerializeField] private GameObject[] lasers;

    Vector2 m_input_movement;
    bool m_input_fire;

    void Start()
    {
        ActivateLasers(false);
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();

        FireLaser();
    }

    private void FireLaser()
    {
        if (m_input_fire)
        {
            ActivateLasers(true);
        }
        else
        {
            ActivateLasers(false);
        }
    }

    private void ActivateLasers(bool active)
    {
        foreach (var laser in lasers)
        {
            var particle_system = laser.GetComponent<ParticleSystem>();
            var emission = particle_system.emission;
            emission.enabled = active;
        }
    }

    private void MovePlayer()
    {
        var currentPos = transform.localPosition;

        currentPos.y += m_input_movement.y * movementSpeed * Time.deltaTime;
        currentPos.x += m_input_movement.x * movementSpeed * Time.deltaTime;

        currentPos.y = Mathf.Clamp(currentPos.y, boarderDown, boarderUp);
        currentPos.x = Mathf.Clamp(currentPos.x, boarderLeft, boarderRight);

        transform.localPosition = currentPos;
    }

    private void RotatePlayer()
    {
        var pitchAngle = m_input_movement.y * -controlPitchFactor;
        pitchAngle += transform.localPosition.normalized.y * -positionPitchFactor;
        var jawAngle = transform.localPosition.normalized.x * -positionYawFactor;
        var rollAngle = m_input_movement.x * -controlRollFactor;

        model.transform.localRotation = Quaternion.Lerp(
            model.transform.localRotation,
            Quaternion.Euler(
                pitchAngle,
                jawAngle,
                rollAngle
            ),
            rotationSpeed * Time.deltaTime
        );
    }

    private void OnMove(InputValue value)
    {
        m_input_movement = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        m_input_fire = value.isPressed;
    }
}
