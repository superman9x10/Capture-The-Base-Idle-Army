using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
    [Header("Character Config")]
    public Storage coinStorage;
    public Storage characterStorage;

    public bool canRandom;

    [Header("Team info")]
    public Material teamColor;
    public int teamNum;
    private void Awake()
    {
        canRandom = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && coinStorage.items.Count < coinStorage.requireAmount)
        {
            GameObject item = other.gameObject;
            item.GetComponent<BoxCollider>().enabled = false;

            //collectedItem = other.gameObject.GetComponent<ItemBase>();

            coinStorage.addItem(item);

            GameManager.instance.itemList.Remove(item);
            //item.SetActive(false);

            item.GetComponent<ItemBase>().holder = coinStorage.transform;

            item.GetComponent<ItemBase>().targetPos = new Vector3(0
                , coinStorage.transform.position.y + coinStorage.items.Count * item.transform.position.y
                , 0);
            item.transform.rotation = coinStorage.transform.rotation;

            item.GetComponent<ItemBase>().doMove();

            canRandom = true;
        }
    }
}
