using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpgradeArea : MonoBehaviour
{
    
    Storage storage;
    public bool canUpgrade = true;
    //[SerializeField] GameObject teammate;
    public List<GameObject> teammate;

    [SerializeField] float timeToSpawn;
    [SerializeField] float timer;

    public GameObject spawnPointGroup;
    public List<Transform> spawnPoints;

    public int count;
    public bool canSpawn = true;

    public AttackArea attackArea;
    public List<GameObject> townList;
    int noOfTown;

    private void Awake()
    {
        townList[noOfTown].SetActive(true);
    }
    private void Start()
    {
        timer = timeToSpawn;
        storage = GetComponentInChildren<Storage>();
        
        float count = spawnPointGroup.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            spawnPoints.Add(spawnPointGroup.transform.GetChild(i));
        }
    }

    private void Update()
    {
        
        if(storage.isFull() && canUpgrade)
        {
            canUpgrade = false;
            //Debug.Log("test");
            
            StartCoroutine("upgradeProcess");
            

        }

        spawn();
        
    }

    void spawn()
    {
        if(canSpawn)
        {
            if (timer <= 0)
            {
                if (count < spawnPoints.Count)
                {
                    GameObject character = Instantiate(teammate[noOfTown], transform.position, Quaternion.identity);
                    attackArea.attackStorage.addItem(character);
                    character.transform.parent = spawnPoints[count].transform;
                    //character.transform.DOMove(spawnPoints[count].position, 1f);
                    character.GetComponent<BotAI>().target = spawnPoints[count];
                    count++;
                }
                else
                {

                    canSpawn = false;
                }

                timer = timeToSpawn;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        
    }
    IEnumerator upgradeProcess()
    {
        Debug.Log("Upgrade succesully");
        townList[noOfTown].SetActive(false);

        if(noOfTown < townList.Count - 1)
        {
            noOfTown++;
        }
            
        townList[noOfTown].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        canUpgrade = true;
        storage.clearItemList();
    }


}
