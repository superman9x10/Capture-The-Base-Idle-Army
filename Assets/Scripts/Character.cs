using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
    [Header("Character Config")]
    public Storage coinStorage;
    public Storage characterStorage;
   

    [Header("Team info")]
    public Material teamColor;
    public int teamNum;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            GameObject item = other.gameObject;
            item.GetComponent<BoxCollider>().enabled = false;
            coinStorage.addItem(item);
            //item.SetActive(false);

            item.GetComponent<ItemBase>().holder = coinStorage.transform;

            item.GetComponent<ItemBase>().targetPos = new Vector3(0
                , coinStorage.transform.position.y + coinStorage.items.Count * item.transform.position.y
                , 0);
            item.transform.rotation = coinStorage.transform.rotation;

            item.GetComponent<ItemBase>().doMove();
        }
    }
}
