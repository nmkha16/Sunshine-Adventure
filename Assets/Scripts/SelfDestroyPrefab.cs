using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyPrefab : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void FixedUpdate()
    {        
        if (transform.position.x < cam.position.x - 22.8f)
        {
            Destroy(gameObject,20);
        }
    }
}
