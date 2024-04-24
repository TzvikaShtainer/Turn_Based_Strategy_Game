using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    [SerializeField] private Transform grenadePrefab;
    [SerializeField] private int maxThrowDistance = 7;
    [SerializeField] private LayerMask obstacleLayerMask;
    
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
    }

    public override string GetActionName()
    {
        return "Grenade";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Transform grenadeProjectileTransform = Instantiate(grenadePrefab, unit.GetWorldPosition(), quaternion.identity);
        GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
        grenadeProjectile.Setup(gridPosition, OnGrenadeBehaviourComplete);
        ActionStart(onActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        
        for (int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for (int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    continue;

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z); //logic for the range of Grenade
                if(testDistance > maxThrowDistance)
                    continue;
                
                // validGridPositionList.Add(testGridPosition); //only to see all the cells of our range
                // continue;
                
                validGridPositionList.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            GridPosition = gridPosition,
            ActionValue = 0,
        };
    }
    
    private void OnGrenadeBehaviourComplete()
    {
        ActionComplete();
    }
}
