using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private int turnNumber;

    public void NextTurn()
    {
        turnNumber++;
    }
}
