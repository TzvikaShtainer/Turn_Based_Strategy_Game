using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    
    private GridPosition _gridPosition;
    
    private bool isActive;
    private float timer;
    private Action onInteractComplete;
    [SerializeField] private bool isGreen = true;

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(_gridPosition, this);
        
        SetColorToGreen();
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

    private void SetColorToGreen()
    {
        isGreen = true;
        meshRenderer.material = greenMaterial;
    }
    
    private void SetColorToRed()
    {
        isGreen = false;
        meshRenderer.material = redMaterial;
    }

    public void Interact(Action onInteractionComplete)
    {
        this.onInteractComplete = onInteractionComplete;
        isActive = true;
        timer = 0.5f;
        
        if (isGreen)
        {
            SetColorToRed();
        }
        else
        {
            SetColorToGreen();
        }
    }
}
