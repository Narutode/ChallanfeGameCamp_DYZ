using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    DateTime start;
    DateTime end;
    public bool isFirst = false;

    // Start is called before the first frame update
    void Start()
    {
        start = DateTime.Now;
        end = start.AddSeconds(5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirst)
        {
            if (DateTime.Now > end)
            {
                Destroy(gameObject);
            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        
    }

}