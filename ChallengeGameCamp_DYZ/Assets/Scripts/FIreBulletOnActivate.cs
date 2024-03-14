using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject Bullets;
    public Transform spawnPoint;
    public float firespeed = 20;
    public int maxBullets = 6;
    private int remainingBullets;
    public TextMeshProUGUI bulletText;
    
    public void Start()
    {
        remainingBullets = maxBullets;
        UpdateBulletUI();
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs args)
    {
        if (remainingBullets > 0)
        {
            GameObject bullet = Instantiate(Bullets);
            bullet.transform.position = spawnPoint.position;
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * firespeed;
            Destroy(bullet, 5);
            remainingBullets--;
            UpdateBulletUI();
            if (remainingBullets == 0)
            {
                StartCoroutine(Reload());
            }
        }
    }
    void UpdateBulletUI()
    {
        bulletText.text = "Bullets: " + remainingBullets.ToString() + " / " + maxBullets.ToString();
    }
    IEnumerator Reload()
    {
        // Attendez un certain temps pour simuler le rechargement
        yield return new WaitForSeconds(2.0f);
        remainingBullets = maxBullets;
    }

}