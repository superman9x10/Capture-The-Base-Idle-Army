using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while(GameManager.instance.gameState == GameManager.GameState.Start)
        {

            GameObject item = ObjectPooling.SharedInstance.GetPooledObject("Item");
            item.transform.parent = transform;
            item.GetComponent<BoxCollider>().enabled = true;
            if (item != null)
            {
                int randX = Random.Range(-20, 21);
                int randZ = Random.Range(-20, 21);
                Vector3 spawnPos = new Vector3(randX, 0.5f, randZ);
                item.transform.position = spawnPos;
                //item.transform.rotation = turret.transform.rotation;
                item.SetActive(true);
            }
            
            GameManager.instance.findItem();
            yield return new WaitForSeconds(3f);
        }
    }
}
