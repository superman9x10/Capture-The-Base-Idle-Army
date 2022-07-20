using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public UpgradeArea upgradeArea;
    public AttackArea attackArea;
    public BuyArea buyArea;

    public int teamNumb;

    private void Awake()
    {
        teamNumb = (int) upgradeArea.teamNumb;
    }
}
