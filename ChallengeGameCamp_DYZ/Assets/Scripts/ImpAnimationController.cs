using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpAnimationController : MonoBehaviour
{
    public Animator impAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            impAnimator.SetBool("isWalking", true);
        }
    }
}
