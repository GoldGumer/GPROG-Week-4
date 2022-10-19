using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.right * Mathf.Lerp(transform.position.x, player.transform.position.x, lerpSpeed)
            + Vector3.forward * Mathf.Lerp(transform.position.z, player.transform.position.z, lerpSpeed) + Vector3.up * transform.position.y;
    }
}
