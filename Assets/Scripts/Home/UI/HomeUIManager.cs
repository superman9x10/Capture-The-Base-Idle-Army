using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeUIManager : MonoBehaviour
{
    public UpgradeArea upgradeArea;
    public BuyArea buyArea;

    public Text buyTextUI;
    public Text upgradeTextUI;
    public Text priceUI;


    private void Start()
    {
        
    }
    private void Update()
    {
        buyTextUI.text = (buyArea.numOfCoins + "/" + buyArea.coinStorage.requireAmount).ToString();

        if (!upgradeArea.isMaxLevel)
        {
            upgradeTextUI.text = (upgradeArea.coinStorage.items.Count + "/" + upgradeArea.coinStorage.requireAmount).ToString();
            
        } else
        {
            upgradeTextUI.text = "Max";
        }

        priceUI.text = "Price: " + upgradeArea.levelPrice[upgradeArea.noOfTown].ToString();
    }

}
