using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnim;
    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        float stoppingDistance = 0.1f;
        if ( Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            //transform.forward = moveDirection; Works Fine but the movement sharp and not smooth
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
            
            unitAnim.SetBool("IsWalking", true);
        }
        else
        {
            unitAnim.SetBool("IsWalking", false);
        }
        
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (gridPosition != newGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        
    }
}
