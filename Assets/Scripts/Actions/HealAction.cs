using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class HealAction : BaseAction
{
    
    [SerializeField] private int healAmount = 30 ;
    [SerializeField] private int maxHealDistance = 1;
    
    [SerializeField] private Transform healVFX;
    
    
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
    }
    
    public override string GetActionName()
    {
        return "Heal";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Unit unitToHeal = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        Instantiate(healVFX, unitToHeal.transform.position + new Vector3(0, 0.5f, 0), quaternion.identity);
        
        HealthSystem unitHealthSystem = unitToHeal.GetComponent<HealthSystem>();
        unitHealthSystem.IncreaseHealth(healAmount);
        
        ActionStart(onActionComplete);
        
        ActionComplete();
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxHealDistance; x <= maxHealDistance; x++)
        {
            for (int z = -maxHealDistance; z <= maxHealDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    continue;
                
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) //gris pos is empty, no unit
                    continue;

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                
                if(!targetUnit.IsEnemy() == unit.IsEnemy()) //both units on same team
                    continue;


                // validGridPositionList.Add(testGridPosition); //only to see all the cells of our range

                validGridPositionList.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        return new EnemyAIAction
        {
            GridPosition = gridPosition,
            ActionValue = 0
        };
    }
    
    public int GetMaxHealDistance()
    {
        return maxHealDistance;
    }
}
