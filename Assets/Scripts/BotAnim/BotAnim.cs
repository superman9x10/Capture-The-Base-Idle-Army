using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnim : MonoBehaviour
{
    [SerializeField] float accelerator;
    [SerializeField] float deaccelerator;

    [SerializeField] Animator anim;
    [SerializeField] BotAI bot;
    int velocityID;
    float curSpeed;
    private void Start()
    {
        velocityID = Animator.StringToHash("velocity");
    }
    private void Update()
    {
        if(bot.velocity != 0)
        {
            anim.SetFloat(velocityID, 1);
            curSpeed = 1;
        } else
        {
            if (curSpeed > 0)
            {
                curSpeed -= deaccelerator * Time.deltaTime;
            }
            anim.SetFloat(velocityID, curSpeed);
        }
    }
}
