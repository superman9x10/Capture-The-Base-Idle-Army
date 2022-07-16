using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretUI : MonoBehaviour
{
    public Text total;
    public Turret turret;
    private void Update()
    {
        total.text = turret.HP.ToString();
    }
}
