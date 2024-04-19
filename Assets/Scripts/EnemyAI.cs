using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float dur = 2f;
    [SerializeField] private float timer;  
    private void Update()
    {
        if(TurnSystem.Instance.IsPlayerTurn())
            return;

        if (timer > dur)
        {
            TurnSystem.Instance.NextTurn();
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
