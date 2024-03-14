using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
<<<<<<< HEAD
    public int linkCount;
=======
    public GameObject parent;
    public int linkCount;
    public int level;
>>>>>>> 02cb60623ccc53fb8498f98de08e313cee594d1e

    private void OnTriggerEnter(Collider other)
    {
        if(!this.CompareTag("Placed"))
        {
            if (other.gameObject.CompareTag("Placed"))
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!this.CompareTag("Placed"))
        {
            if (other.gameObject.CompareTag("Placed"))
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
<<<<<<< HEAD
=======
        Debug.Log("aaaaaaaaaaaaaaaa");
>>>>>>> 02cb60623ccc53fb8498f98de08e313cee594d1e
        if (!this.CompareTag("Placed"))
        {
            if (collision.gameObject.CompareTag("Placed"))
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
<<<<<<< HEAD
=======
        Debug.Log("aaaaaaaaaaaaaaaa");
>>>>>>> 02cb60623ccc53fb8498f98de08e313cee594d1e
        if (!this.CompareTag("Placed"))
        {
            if (collision.gameObject.CompareTag("Placed"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
