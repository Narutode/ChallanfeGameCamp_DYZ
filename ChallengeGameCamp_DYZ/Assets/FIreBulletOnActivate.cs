using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FIreBulletOnActivate : MonoBehaviour
{
    public GameObject Bullets;
    public Transform spawnPoint;
    public float firespeed = 20;

    public void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs args)
    {
        GameObject bullet = Instantiate(Bullets);
        bullet.transform.position = spawnPoint.position;
        bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * firespeed;
        Destroy(bullet,5);
    }
}
