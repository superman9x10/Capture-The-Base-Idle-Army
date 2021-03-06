using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : AreaBase
{
    public Storage botReadyStorage;
    public Transform target;

    public Storage teammateStorage;

    public BuyArea buyArea;
    public Character character;
    private void Update()
    {
        botFollowCharacterProcess();
    }

    void botFollowCharacterProcess()
    {
        if (isStanding)
        {
            if (timer <= 0)
            {
                for (int i = 0; i < botReadyStorage.items.Count; i++)
                {
                    botReadyStorage.items[i].GetComponent<BotAI>().target = target.transform;
                    botReadyStorage.items[i].GetComponent<BotAI>().bot.stoppingDistance = 3;
                    botReadyStorage.items[i].GetComponent<BotAI>().botTeamNum = character.teamNum;

                    teammateStorage.addItem(botReadyStorage.items[i]);
                }
                
                botReadyStorage.clearItemList();

                buyArea.spawnPointIdx = 0;
                buyArea.canSpawn = true;

                timer = startActionTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") && (int)teamNumb == other.GetComponent<Character>().teamNum)
        {
            isStanding = true;
            target = other.transform;
            character = other.GetComponent<Character>();
            teammateStorage = other.GetComponent<Character>().characterStorage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isStanding = false;
            timer = startActionTime;
        }
    }
}
