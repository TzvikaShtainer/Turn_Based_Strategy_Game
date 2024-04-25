using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private Animator doorAnim;
    private GridPosition _gridPosition;
    private Action onInteractComplete;
    private bool isActive;
    private float timer;

    private void Awake()
    {

        doorAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetDoorAtGridPosition(_gridPosition, this);

        if (isOpen)
            Open();
        else
            Close();
    }

    private void Update()
    {
        if (!isActive)
            return;
        
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isActive = false;
            onInteractComplete();
        }
    }

    public void Interact(Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = 0.5f;
        
        if (isOpen)
            Close();
        else
            Open();
    }

    public void Open()
    {
        isOpen = true;
        doorAnim.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetWalkableGridPosition(_gridPosition, true);
    }
    
    public void Close()
    {
        isOpen = false;
        doorAnim.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetWalkableGridPosition(_gridPosition, false);
    }
}
