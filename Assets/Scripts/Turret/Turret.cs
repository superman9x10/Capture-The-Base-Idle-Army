using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Turret : MonoBehaviour
{
    [Header("Turret info")]
    public Storage turretStorage;
    public Storage characterStorage;
    public int HP;

    [Header("Turret")]
    public bool isStanding;
    public float timer;
    public float startActionTime;

    [Header("Land list")]
    public List<GameObject> listOfLands;

    //public bool isOccupied;

    public Character character;
    public int ownerTeam;
    public Material occupiedTeamColor;

    private void Awake()
    {
        timer = startActionTime;
    }

    private void Update()
    {
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
            
            turretStorage.addItem(bot);
            characterStorage.removeItem(bot);

            yield return new WaitForSeconds(0.05f);
            
        }

        StartCoroutine("changeColor");
    }

    IEnumerator changeColor()
    {
        //yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = occupiedTeamColor.color;
        for (int i = 0; i < listOfLands.Count; i++)
        {
            listOfLands[i].GetComponent<Renderer>().material.DOColor(occupiedTeamColor.color, 1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            
            isStanding = true;
            characterStorage = other.GetComponent<Character>().characterStorage;
            occupiedTeamColor = other.GetComponent<Character>().teamColor;

            character = other.GetComponent<Character>();
            
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