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
        upgradeTextUI.text = (upgradeArea.coinStorage.items.Count + "/" + upgradeArea.coinStorage.requireAmount).ToString();
        buyTextUI.text = (buyArea.coinStorage.items.Count + "/" + buyArea.coinStorage.requireAmount).ToString();
    }

}
