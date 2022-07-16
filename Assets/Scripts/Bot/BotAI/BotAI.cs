using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotAI : MonoBehaviour
{
    public NavMeshAgent bot;
    public Transform target;
    
    public float velocity;

    public bool isGoToTurret;
    public BoxCollider collider;
    public int price;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("trigger") && isGoToTurret)
        {
            Turret turret = other.GetComponentInParent<Turret>();
            turret.HP += (int)gameObject.GetComponent<BotConfig>().level + 1;
            isGoToTurret = false;
            gameObject.SetActive(false);
        }
    }
}
