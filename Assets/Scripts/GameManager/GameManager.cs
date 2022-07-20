using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject itemGroup;
    [Header("All target list")]
    public List<GameObject> itemList;
    public List<GameObject> turretList;
    public List<GameObject> homeList;


    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        findProcess();
    }

    public enum GameState
    {
        Ready,
        Start,
        Win,
        Lose
    }

    public GameState gameState;


    void findProcess()
    {
        findItem();
        findTurret();
        findHome();
    }

    public void findItem()
    {
        itemList.Clear();
        int itemCurAmount = itemGroup.transform.childCount;
        for (int i = 0; i < itemCurAmount; i++)
        {
            itemList.Add(itemGroup.transform.GetChild(i).gameObject);
        }
    }

    public void findTurret()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turretList.Add(turrets[i]);
        }
    }

    public void findHome()
    {
        GameObject[] homes = GameObject.FindGameObjectsWithTag("Home");
        for (int i = 0; i < homes.Length; i++)
        {
            homeList.Add(homes[i]);
        }
    }

}
