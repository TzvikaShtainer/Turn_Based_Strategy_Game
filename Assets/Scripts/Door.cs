using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public static event EventHandler OnAnyDoorOpened;
    public  event EventHandler OnDoorOpened;
    
    [SerializeField] private bool isOpen = true;
    
    private Animator doorAnim;
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
        LevelGrid.Instance.SetInteractableAtGridPosition(_gridPosition, this);

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
        
        OnDoorOpened?.Invoke(this, EventArgs.Empty);
        
        OnAnyDoorOpened?.Invoke(this, EventArgs.Empty);
    }
    
    public void Close()
    {
        isOpen = false;
        doorAnim.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetWalkableGridPosition(_gridPosition, false);
    }
}
