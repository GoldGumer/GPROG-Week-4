using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationPerSec;
    [SerializeField] private float alertRange;
    [SerializeField] private float chaseChance;
    [SerializeField] private string[] states;
    [SerializeField] private float runAwaySetTime;

    private float runAwayCurrentTime = 0f;
    private int currentState;
    private GameObject player;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        currentState = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (states[currentState] == "Patrol")
        {
            transform.position += transform.forward * moveSpeed;
            transform.Rotate(0, rotationPerSec, 0);
        }
        else if (states[currentState] == "Alert")
        {
            transform.LookAt(player.transform);
            if (Random.Range(0.0f,1.0f) < chaseChance)
            {
                currentState = 2;
            }
        }
        else if (states[currentState] == "Chase")
        {
            transform.position += transform.forward * moveSpeed;
            transform.LookAt(player.transform);

        }
        else if (states[currentState] == "Run Away")
        {
            transform.position += transform.forward * moveSpeed;
        }
        else if (states[currentState] == "Shoot")
        {

        }
        if (runAwayCurrentTime <= 0f)
        {
            currentState = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("TearGas"))
        {
            currentState = 3;
            transform.Rotate(0, 90f, 0);
            runAwayCurrentTime = runAwaySetTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = 0;
        }
    }
}
