using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Storage characterStorage;
    public Storage teammateStorage;

    public bool isStandOnUpgradeArea;
    public bool isStandOnAttackArea;
    public bool isStandOnTurretArea;

    public float timeToBeginUpgrade;
    public float timer;

    Storage upgradeAreaStorage;
    Storage attackAreaStorage;
    Storage turretStorage;
    UpgradeArea upgradeArea;

    private void Start()
    {
        timer = timeToBeginUpgrade;
    }

    protected void Update()
    {
        upgradeHandle();
        teammateFollowPlayerHandle();
        attackTurretHandle();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("UpgradeArea"))
        {
            isStandOnUpgradeArea = false;
            timer = timeToBeginUpgrade;
        }

        if (other.CompareTag("AttackArea"))
        {
            isStandOnAttackArea = false;
            timer = timeToBeginUpgrade;
        }

        if (other.CompareTag("Turret"))
        {
            isStandOnTurretArea = false;
            timer = timeToBeginUpgrade;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            GameObject item = other.gameObject;
            characterStorage.addItem(item);
            item.SetActive(false);
        }

        if(other.CompareTag("UpgradeArea"))
        {
            isStandOnUpgradeArea = true;
            upgradeAreaStorage = other.GetComponentInChildren<Storage>();
            upgradeArea = other.GetComponent<UpgradeArea>();

        }

        if(other.CompareTag("AttackArea"))
        {
            isStandOnAttackArea = true;
            attackAreaStorage = other.GetComponentInChildren<Storage>();
            upgradeArea = other.transform.parent.GetChild(0).GetComponent<UpgradeArea>();
        }

        if (other.CompareTag("Turret"))
        {
            isStandOnTurretArea = true;
            turretStorage = other.GetComponentInChildren<Storage>();
        }
    }

    void teammateFollowPlayerHandle()
    {
        if (isStandOnAttackArea)
        {
            if (timer <= 0)
            {
                for (int i = 0; i < attackAreaStorage.items.Count; i++)
                {
                    attackAreaStorage.items[i].GetComponent<BotAI>().target = this.transform;
                    attackAreaStorage.items[i].GetComponent<BotAI>().bot.stoppingDistance = 3;

                    teammateStorage.addItem(attackAreaStorage.items[i]);
                }
                //isStandOnAttackArea = false;
                attackAreaStorage.clearItemList();
                
                upgradeArea.canSpawn = true;
                upgradeArea.count = 0;
                timer = timeToBeginUpgrade;
            }
            else
            {
                timer -= Time.deltaTime;
            }
            
        }
    }

    void upgradeHandle()
    {
        transferProcess(isStandOnUpgradeArea, characterStorage, upgradeAreaStorage);
    }

    void attackTurretHandle()
    {
        transferProcess(isStandOnTurretArea, teammateStorage, turretStorage);
    } 

    void transferProcess(bool areaThatPlayerAreStanding, Storage fromStorage, Storage targetStorage)
    {
        if (areaThatPlayerAreStanding)
        {
            if (timer < 0)
            {
                if (!fromStorage.isEmpty())
                {
                    
                    StartCoroutine(ie_transfer(targetStorage, fromStorage));
                }

                timer = timeToBeginUpgrade;

            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    IEnumerator ie_transfer(Storage target, Storage from)
    {

        while (!target.isFull() && !from.isEmpty())
        {
            
            GameObject item = from.items[0];
            item.SetActive(false);
            target.addItem(item);
            from.removeItem(item);
            
            yield return new WaitForSeconds(0.05f);
            
        }


    }
}
