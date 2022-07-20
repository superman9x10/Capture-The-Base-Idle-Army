using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class BotController : Character
{
    public AIPath aIPath;
    public enum levelAI
    {
        level_1,
        level_2,
        level_3
    }

    public levelAI botLevel;

    public enum BotState
    {
        buy,
        upgrade,
        take,
        collect,
        atkTurret,
        atkCharacter
    }

    public BotState botState;


    private void Start()
    {
        switch(botLevel)
        {
            case levelAI.level_1:
                {
                    StartCoroutine("botLevel_1_Handle");
                    break;
                }
            case levelAI.level_2:
                {
                    StartCoroutine("botLevel_2_Handle");
                    break;
                }
            case levelAI.level_3:
                {
                    StartCoroutine("botLevel_3_Handle");
                    break;
                }
        }
        

    }

    private void Update()
    {
        //collectItem();
    }

    IEnumerator botLevel_1_Handle()
    {
        while (GameManager.instance.gameState == GameManager.GameState.Start)
        {

            switch (botState)
            {
                case BotState.collect:
                    {
                        yield return StartCoroutine("collectItemProcess");
                        botState = BotState.buy;

                        break;
                    }
                case BotState.buy:
                    {
                        yield return StartCoroutine("buyProcess");
                        //botState = BotState.collect;
                        if(characterStorage.items.Count < 5)
                        {
                            int randNum = Random.Range(0, 3);
                            if(randNum < 1)  //----
                            {
                                botState = BotState.collect;
                            } else
                            {
                                botState = BotState.take;
                            }
                        } else
                        {
                            botState = BotState.collect;
                        }

                        break;
                    }
                case BotState.take:
                    {
                        yield return StartCoroutine("teamFollowProcess");
                        //botState = BotState.collect;

                        if(characterStorage.items.Count > 1)
                        {
                            botState = BotState.atkTurret;
                        } else
                        {
                            int randNum = Random.Range(0, 3);
                            if (randNum < 2)
                            {
                                botState = BotState.collect;
                            }
                            else
                            {
                                botState = BotState.take;
                            }
                        }

                        break;
                    }
                case BotState.atkTurret:
                    {
                        yield return StartCoroutine("atkTurret");
                        
                        botState = BotState.collect;

                        break;
                    }
            }

            yield return new WaitForSeconds(0.5f);
            
        }
    }

    IEnumerator botLevel_2_Handle()
    {
        while (GameManager.instance.gameState == GameManager.GameState.Start)
        {
            switch (botState)
            {
                case BotState.collect:
                    {
                        StartCoroutine("collectItemProcess");
                        break;
                    }
                //case BotState.atkCharacter:
                //    {
                //        StartCoroutine("atkOtherCharacterProcess");
                //        break;
                //    }
                //case BotState.atkTurret:
                //    {
                //        StartCoroutine("atkTurret");
                //        break;
                //    }
                case BotState.buy:
                    {
                        StartCoroutine("buyProcess");
                        break;
                    }
                //case BotState.upgrade:
                //    {
                //        StartCoroutine("upgradeBase");
                //        break;
                //    }
                case BotState.take:
                    {
                        StartCoroutine("teamFollowProcess");
                        break;
                    }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator botLevel_3_Handle()
    {
        while (GameManager.instance.gameState == GameManager.GameState.Start)
        {
            switch (botState)
            {
                case BotState.collect:
                    {
                        StartCoroutine("collectItemProcess");
                        break;
                    }
                //case BotState.atkCharacter:
                //    {
                //        StartCoroutine("atkOtherCharacterProcess");
                //        break;
                //    }
                //case BotState.atkTurret:
                //    {
                //        StartCoroutine("atkTurret");
                //        break;
                //    }
                case BotState.buy:
                    {
                        StartCoroutine("buyProcess");
                        break;
                    }
                //case BotState.upgrade:
                //    {
                //        StartCoroutine("upgradeBase");
                //        break;
                //    }
                case BotState.take:
                    {
                        StartCoroutine("teamFollowProcess");
                        break;
                    }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    //==================In===========================
    IEnumerator buyProcess()
    {
        for(int i = 0; i < GameManager.instance.homeList.Count; i++)
        {
            HomeManager homeTeamNumber = GameManager.instance.homeList[i].GetComponent<HomeManager>();
            if (homeTeamNumber.teamNumb == teamNum)
            {
                aIPath.destination = homeTeamNumber.buyArea.transform.position;
            }
        }

        while (!aIPath.reachedDestination)
        {
            yield return null;
        }


        yield return new WaitForSeconds(1.2f);
    }

    IEnumerator upgradeBase()
    {
        yield return null;
    }

    IEnumerator teamFollowProcess()
    {
        for (int i = 0; i < GameManager.instance.homeList.Count; i++)
        {
            HomeManager homeTeamNumber = GameManager.instance.homeList[i].GetComponent<HomeManager>();
            if (homeTeamNumber.teamNumb == teamNum)
            {
                aIPath.destination = homeTeamNumber.attackArea.transform.position;
            }
        }

        while (!aIPath.reachedDestination)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        
    }

    //==================Out=========================
    IEnumerator collectItemProcess()
    {
        while (coinStorage.items.Count < coinStorage.requireAmount)
        {
            if(GameManager.instance.itemList.Count != 0)
            {   
                if (canRandom)
                {                    
                    canRandom = false;
                    int randIdx = Random.Range(0, GameManager.instance.itemList.Count);
                    aIPath.destination = GameManager.instance.itemList[randIdx].transform.position;
                } 
                if(aIPath.reachedEndOfPath)
                {
                    canRandom = true;
                }
                
            }

            yield return null;
        }
        yield return null;
    }

    IEnumerator atkTurret()
    {
        int randNum = Random.Range(0, GameManager.instance.turretList.Count);
        aIPath.destination = GameManager.instance.turretList[randNum].transform.position;

        while (aIPath.remainingDistance > 1f)
        {
            
            yield return null;
        }
        Debug.Log("den dich");
        yield return new WaitForSeconds(1.2f);
    }

    IEnumerator atkOtherCharacterProcess()
    {
        yield return null;
    }

}
