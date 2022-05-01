using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveEnemy : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
