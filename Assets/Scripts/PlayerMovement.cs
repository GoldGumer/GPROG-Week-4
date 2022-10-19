using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject tearGas;
    [SerializeField] private float tearGasSetCooldown;

    private float tearGasCurrentCooldown = 0f;

    void FixedUpdate()
    {
        transform.position += (Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical")) * speed;
        if (Input.GetAxis("Jump") > 0.2f && tearGasCurrentCooldown <= 0f)
        {
            Instantiate(tearGas, transform.position, Quaternion.identity);
            tearGasCurrentCooldown = tearGasSetCooldown;
        }
        tearGasCurrentCooldown -= Time.deltaTime;
    }
}
