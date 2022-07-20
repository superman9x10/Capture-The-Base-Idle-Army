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

    public int botTeamNum;
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

            turretHPchangeProcess(other);

            isGoToTurret = false;
            gameObject.SetActive(false);
        }
    }

    void turretHPchangeProcess(Collider other)
    {
        Turret turret = other.GetComponentInParent<Turret>();

        if (turret.HP == 0)
        {
            turret.ownerTeam = turret.character.teamNum;
            turret.HP += (int)gameObject.GetComponent<BotConfig>().level + 1;
            //Debug.Log("Change color");
            //StartCoroutine(turret.changeColor());
        }
        else
        {
            if (botTeamNum == turret.ownerTeam)
            {
                turret.HP += (int)gameObject.GetComponent<BotConfig>().level + 1;
            }
            else
            {
                turret.HP -= (int)gameObject.GetComponent<BotConfig>().level + 1;
            }
        }
    }
}
