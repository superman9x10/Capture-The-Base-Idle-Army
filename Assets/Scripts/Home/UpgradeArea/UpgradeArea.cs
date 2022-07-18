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
    public int[] levelPrice;
    public bool isMaxLevel;
    private void Awake()
    {
        timer = startActionTime;
        //townList[noOfTown].SetActive(true);
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

        if(noOfTown == townList.Count - 1)
        {
            isMaxLevel = true;
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
            GameObject item = characterStorage.items[characterStorage.items.Count - 1];

            item.GetComponent<ItemBase>().holder = coinStorage.transform;
            item.GetComponent<ItemBase>().targetPos = new Vector3(0
                , coinStorage.transform.position.y
                , 0);
            item.GetComponent<ItemBase>().doMove();


            
            coinStorage.addItem(item);
            characterStorage.removeItem(item);
            
            yield return new WaitForSeconds(0.1f);
            item.SetActive(false);
        }
       // isStanding = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Character") && (int)teamNumb == other.GetComponent<Character>().teamNum)
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