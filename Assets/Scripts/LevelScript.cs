using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> hider1List;
    [SerializeField] private List<GameObject> hider2List;
    [SerializeField] private List<GameObject> hider3List;
    [SerializeField] private List<GameObject> hider4List;
    [SerializeField] private List<GameObject> hider5List;
    
    [SerializeField] private List<GameObject> enemy1List;
    [SerializeField] private List<GameObject> enemy2List;
    [SerializeField] private List<GameObject> enemy3List;
    
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;
    [SerializeField] private Door door3;
    [SerializeField] private Door door4;
    [SerializeField] private Door door5;

    private float enemyTurnTimer = 0;
    private float enemyTurnTime = 2;
    
    private bool hasShownFirstHider = false;
    
    private void Start()
    {
        SetActiveGameObjectList(enemy1List, false);
        SetActiveGameObjectList(enemy2List, false);
        SetActiveGameObjectList(enemy3List, false);
        
        LevelGrid.Instance.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;
        
        door1.OnDoorOpened += (object sender, EventArgs e) =>
        {
            SetActiveGameObjectList(hider1List, false);
            SetActiveGameObjectList(enemy1List, true);
        };
        
        door2.OnDoorOpened += (object sender, EventArgs e) =>
        {
            SetActiveGameObjectList(hider2List, false);
            SetActiveGameObjectList(enemy2List, true);
        };
        
        door3.OnDoorOpened += (object sender, EventArgs e) =>
        {
            SetActiveGameObjectList(hider3List, false);
            SetActiveGameObjectList(enemy3List, true);
        };
        
        door4.OnDoorOpened += (object sender, EventArgs e) =>
        {
            SetActiveGameObjectList(hider4List, false);
        };
        
        door5.OnDoorOpened += (object sender, EventArgs e) =>
        {
            SetActiveGameObjectList(hider5List, false);
        };
    }

    private void Update()
    {
        if(TurnSystem.Instance.IsPlayerTurn())
            return;
        
        enemyTurnTimer += Time.deltaTime;
        
        if (!TurnSystem.Instance.IsPlayerTurn() && 
            !GetGameObjectListState(enemy1List) && !GetGameObjectListState(enemy2List) && !GetGameObjectListState(enemy3List)
            && enemyTurnTimer >= enemyTurnTime)
        {
            enemyTurnTimer = 0;
            
            TurnSystem.Instance.NextTurn();
        }
    }

    private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, LevelGrid.OnAnyUnitMovedGridPositionEventArgs e)
    {

        // if (e.toGridPosition.z == 5 && !hasShownFirstHider)
        // {
        //     hasShownFirstHider = true;
        //     //SetActiveGameObjectList(hider1List, false);
        //     SetActiveGameObjectList(enemy1List, true);
        // }
    }

    private void SetActiveGameObjectList(List<GameObject> gameObjectList, bool isActive)
    {
        foreach (GameObject gameObject in gameObjectList)
        {
            gameObject.SetActive(isActive);
        }
    }

    private bool GetGameObjectListState(List<GameObject> gameObjectList)
    {
        foreach (GameObject gameObject in gameObjectList)
        {
            if (gameObject == null)
                continue;
            
            if (gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

}
