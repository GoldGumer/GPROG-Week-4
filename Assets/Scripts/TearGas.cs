using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearGas : MonoBehaviour
{
    [SerializeField] private float lingerTime;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lingerTime -= Time.deltaTime;
        if (lingerTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
