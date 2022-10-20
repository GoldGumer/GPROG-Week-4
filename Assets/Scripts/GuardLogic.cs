using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationPerSec;
    [SerializeField] private float alertRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseChance;
    [SerializeField] private string[] states;
    [SerializeField] private float runAwaySetTime;
    [SerializeField] private float sightAngle;

    private float runAwayCurrentTime;
    private int currentState;
    private GameObject player;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        currentState = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        runAwayCurrentTime = runAwaySetTime;
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        float angleToPlayer = Vector3.Angle(transform.forward, ray.direction);
        RaycastHit rayHit;
        if (currentState == 0 && angleToPlayer <= sightAngle && angleToPlayer >= -sightAngle
            && Physics.Raycast(ray, out rayHit, alertRange) && rayHit.transform.CompareTag("Player")) currentState = 1;
        else if ((currentState == 2 || currentState == 1) && angleToPlayer <= sightAngle && angleToPlayer >= -sightAngle
            && !Physics.Raycast(ray, chaseRange)) currentState = 0;

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
            runAwayCurrentTime -= Time.deltaTime;
        }
        else if (states[currentState] == "Shoot")
        {

        }
        if (runAwayCurrentTime <= 0f)
        {
            currentState = 0;
            runAwayCurrentTime = runAwaySetTime;
        }
    }
}
