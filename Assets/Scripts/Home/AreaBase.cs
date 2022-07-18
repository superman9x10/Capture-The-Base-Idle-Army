using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBase : MonoBehaviour
{
    [Header("AreaBase Config")]
    public float timer;
    public float startActionTime;
    public bool isStanding;

    public enum Team
    {
        team_1,
        team_2,
        team_3
    }
    public Team teamNumb;

    private void Awake()
    {
        timer = startActionTime;
    }
}
