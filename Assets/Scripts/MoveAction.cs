using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnim;
    
    private Vector3 targetPosition;
    
    private void Awake()
    {
        targetPosition = transform.position;
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
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
