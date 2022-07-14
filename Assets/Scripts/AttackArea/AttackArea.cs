using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public Storage attackStorage;

    private void Start()
    {
        attackStorage = GetComponentInChildren<Storage>();
    }
}
