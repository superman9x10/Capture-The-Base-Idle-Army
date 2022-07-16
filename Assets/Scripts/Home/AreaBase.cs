using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBase : MonoBehaviour
{
    [Header("AreaBase Config")]
    public float timer;
    public float startActionTime;
    public bool isStanding;

    private void Awake()
    {
        timer = startActionTime;
    }

    
}
