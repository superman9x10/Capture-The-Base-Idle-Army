using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Storage turretStorage;
    public Storage characterStorage;
    public int HP;
    public bool isStanding;
    public float timer;
    public float startActionTime;

    
    private void Awake()
    {
        timer = startActionTime;
    }

    private void Update()
    {
        //if(!turretStorage.isEmpty())
        //{
        //    //HP += (int) turretStorage.items[0].GetComponent<BotConfig>().level + 1;
        //    //turretStorage.clearItemList();
        //}

        transfeProcess();
    }

    void transfeProcess()
    {
        if (isStanding)
        {
            if (timer <= 0)
            {
                isStanding = false;
                if (!characterStorage.isEmpty())
                {
                    StartCoroutine(ie_transfer());
                }

                timer = startActionTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    IEnumerator ie_transfer()
    {
        while (!turretStorage.isFull() && !characterStorage.isEmpty())
        {
            GameObject bot = characterStorage.items[0];
            BotAI botAI = bot.GetComponent<BotAI>();
            
            botAI.target = this.transform;
            botAI.bot.stoppingDistance = 0;
            botAI.isGoToTurret = true;
            botAI.collider.enabled = true;
            //item.SetActive(false);
            turretStorage.addItem(bot);
            characterStorage.removeItem(bot);

            //HP += (int)turretStorage.items[0].GetComponent<BotConfig>().level + 1;
            //turretStorage.clearItemList();
            yield return new WaitForSeconds(0.05f);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isStanding = true;
            characterStorage = other.GetComponent<Character>().characterStorage;
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