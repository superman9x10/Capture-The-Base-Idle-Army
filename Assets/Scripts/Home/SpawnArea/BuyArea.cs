using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyArea : AreaBase
{
    [Header("BuyArea Config")]
    public Storage coinStorage;
    Storage characterStorage;

    [Header("Spawn Condition")]
    public UpgradeArea upgradeArea;
    public bool canSpawn;
    public float delayTime;
    public float timeToSpawn;

    [Header("SpawnInfo")]
    public GameObject spawnPointGroup;
    public List<Transform> spawnPoints;
    public List<GameObject> characterToSpawn;
    public int spawnPointIdx;
    public int numOfCoins;
    public Transform spawnPos;

    [Header("ReadyAtk Storage")]
    public Storage readyAtkStorage;


    private void Start()
    {
        float count = spawnPointGroup.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            spawnPoints.Add(spawnPointGroup.transform.GetChild(i));
        }
    }
    private void Update()
    {

        transfeProcess();
        spawn();
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
        while (!coinStorage.isFull() && !characterStorage.isEmpty())
        {
            GameObject item = characterStorage.items[0];
            item.SetActive(false);
            coinStorage.addItem(item);
            characterStorage.removeItem(item);

            yield return new WaitForSeconds(0.05f);
            
        }
        //isStanding = true;
        canSpawn = true;
        numOfCoins = coinStorage.items.Count;
    }

    void spawn()
    {
        if (canSpawn)
        {
            if (delayTime <= 0)
            {
                BotAI bot = characterToSpawn[upgradeArea.noOfTown].GetComponent<BotAI>();
                if (spawnPointIdx < spawnPoints.Count && numOfCoins - bot.price >= 0)
                {
                    GameObject character = Instantiate(characterToSpawn[upgradeArea.noOfTown], spawnPos.position, Quaternion.identity);
                    readyAtkStorage.addItem(character);
                    coinStorage.removeItem(coinStorage.items[0]);

                    character.transform.parent = spawnPoints[spawnPointIdx].transform;
                        
                    character.GetComponent<BotAI>().target = spawnPoints[spawnPointIdx];

                    spawnPointIdx++;
                    numOfCoins--;
                }
                else
                {
                    canSpawn = false;
                    
                }

                delayTime = timeToSpawn;
            }
            else
            {
                delayTime -= Time.deltaTime;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isStanding = true;
            characterStorage = other.GetComponent<Character>().coinStorage;
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
