using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVfxPrefab;
    
    private Vector3 targetPosition;
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
        
        float moveSpeed = 200f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        
        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceBeforeMoving < distanceAfterMoving) //(Vector3.Distance(transform.position, targetPosition) < 0.1f)  not good for fast objects
        {
            transform.position = targetPosition; // so the bullet wont go after the head
            
            trailRenderer.transform.parent = null;

            Instantiate(bulletHitVfxPrefab, targetPosition, quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
