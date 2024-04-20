using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    
    private List<Unit> unitList;
    private List<Unit> friendlyList;
    private List<Unit> enemyList;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        
        unitList = new List<Unit>();
        friendlyList = new List<Unit>();
        enemyList = new List<Unit>();
    }

    private void Start() //potential bug: execution order - add this script in the "execution order" in settings before defualt Time
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        
        unitList.Add(unit);

        Debug.Log(unit);
        if (unit.IsEnemy())
        {
            enemyList.Add(unit);
        }
        else
        {
            friendlyList.Add(unit);
        }
    }
    
    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        
        unitList.Remove(unit);

        if (unit.IsEnemy())
        {
            enemyList.Remove(unit);
        }
        else
        {
            friendlyList.Remove(unit);
        }
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }
    
    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyList;
    }
    
    public List<Unit> GetEnemyUnitList()
    {
        return enemyList;
    }
}
