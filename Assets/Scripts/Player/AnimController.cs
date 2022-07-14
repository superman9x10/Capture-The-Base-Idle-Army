using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] float accelerator;
    [SerializeField] float deaccelerator;

    [SerializeField] Animator anim;
    [SerializeField] PlayerController playerController;

    int velocityID;
    float curSpeed;
    private void Start()
    {
        velocityID = Animator.StringToHash("velocity");
    }
    private void Update()
    {
        if(playerController.velocity != 0)
        {
            anim.SetFloat(velocityID, 1);
            curSpeed = 1;
        } else
        {
            if(curSpeed >  0)
            {
                curSpeed -= deaccelerator * Time.deltaTime;
            }
            anim.SetFloat(velocityID, curSpeed);
        }
    }

}
