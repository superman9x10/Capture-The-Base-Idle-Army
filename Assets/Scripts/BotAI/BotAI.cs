using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotAI : MonoBehaviour
{
    public NavMeshAgent bot;
    public Transform target;

    public float velocity;
    private void Start()
    {
        bot = GetComponent<NavMeshAgent>();
        bot.SetDestination(target.position);
    }
    private void Update()
    {
        if(target != null)
        {
            bot.SetDestination(target.position);
        }

        if (bot.remainingDistance > bot.stoppingDistance)
        {
            velocity = 1;
        }
        else
        {
            velocity = 0;
        }

        
    }
}
