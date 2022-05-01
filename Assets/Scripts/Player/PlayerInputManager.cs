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

    Vector2 m_input_movement;

    void Start()
    {
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
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
        var pitchAngle = m_input_movement.y * -controlRollFactor;
        pitchAngle += transform.localPosition.normalized.y * -positionPitchFactor;
        var jawAngle = transform.localPosition.normalized.x * -positionYawFactor;
        var rollAngle = m_input_movement.x * -controlPitchFactor;

        transform.localRotation = Quaternion.Euler(
            pitchAngle,
            jawAngle,
            rollAngle
        );
    }

    private void OnMove(InputValue value)
    {
        m_input_movement = value.Get<Vector2>();
    }
}
