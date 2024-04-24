using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{

    private Vector3 targetPosition;
    private Action OnGrenadeBehaviourComplete;
    private float moveSpeed = 15f;

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float reachedTargetDis = 0.2f;
        if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDis)
        {
            float damageRadius = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(30);
                }
            }
            
            Destroy(gameObject);

            OnGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action OnGrenadeBehaviourComplete)
    {
        this.OnGrenadeBehaviourComplete = OnGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}
