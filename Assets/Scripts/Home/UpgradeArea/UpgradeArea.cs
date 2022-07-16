using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradeArea : AreaBase
{
    [Header("UpgradeArea Config")]
    public Storage coinStorage;
    public Storage characterStorage;
    public List<GameObject> townList;
    public int noOfTown;

    public bool canUpgrade;
    private void Awake()
    {
        timer = startActionTime;
        townList[noOfTown].SetActive(true);
        canUpgrade = true;
    }

    private void Update()
    {
        transfeProcess();
        upgradeProcess();
        
    }
    void transfeProcess()
    {
        if (isStanding && noOfTown < townList.Count - 1)
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

    void upgradeProcess()
    {
        if (coinStorage.isFull() && canUpgrade)
        {
            canUpgrade = false;
            Debug.Log("test");

            StartCoroutine("upgradeHandle");

        }
    }

    IEnumerator upgradeHandle()
    {
        Debug.Log("Upgrade succesully");
        townList[noOfTown].SetActive(false);

        if (noOfTown < townList.Count - 1)
        {
            noOfTown++;
        }

        townList[noOfTown].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        canUpgrade = true;
        //isStanding = true;
        coinStorage.clearItemList();
    }

    IEnumerator ie_transfer()
    {
        while (!coinStorage.isFull() && !characterStorage.isEmpty())
        {
            GameObject item = characterStorage.items[0];
            item.SetActive(false);
            coinStorage.addItem(item);
            characterStorage.removeItem(item);
            
            yield return new WaitForSeconds(0.1f);
            
        }
       // isStanding = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
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


    //Storage storage;
    //public bool canUpgrade = true;
    //public List<GameObject> teammate;


    //public GameObject spawnPointGroup;
    //public List<Transform> spawnPoints;

    //public int count;

    //public AttackArea attackArea;
    //public List<GameObject> townList;
    //public int noOfTown;

    //private void Awake()
    //{
    //    townList[noOfTown].SetActive(true);
    //}
    //private void Start()
    //{
    //    storage = GetComponentInChildren<Storage>();

    //    float count = spawnPointGroup.transform.childCount;
    //    for(int i = 0; i < count; i++)
    //    {
    //        spawnPoints.Add(spawnPointGroup.transform.GetChild(i));
    //    }
    //}

    //private void Update()
    //{

    //    if(storage.isFull() && canUpgrade)
    //    {
    //        canUpgrade = false;
    //        //Debug.Log("test");

    //        StartCoroutine("upgradeProcess");

    //    }

    //    //spawn();

    //}

    ////void spawn()
    ////{
    ////    if(canSpawn)
    ////    {
    ////        if (timer <= 0)
    ////        {
    ////            if (count < spawnPoints.Count && itemNumb > 0)
    ////            {
    ////                GameObject character = Instantiate(teammate[noOfTown], transform.position, Quaternion.identity);
    ////                attackArea.attackStorage.addItem(character);
    ////                character.transform.parent = spawnPoints[count].transform;
    ////                //character.transform.DOMove(spawnPoints[count].position, 1f);
    ////                character.GetComponent<BotAI>().target = spawnPoints[count];
    ////                count++;
    ////                itemNumb--;
    ////            }
    ////            else
    ////            {

    ////                canSpawn = false;
    ////            }

    ////            timer = timeToSpawn;
    ////        }
    ////        else
    ////        {
    ////            timer -= Time.deltaTime;
    ////        }
    ////    }

    ////}



}