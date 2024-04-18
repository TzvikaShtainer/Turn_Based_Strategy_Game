using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayer;

    private BaseAction selectedAction;
    private bool isBusy;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if(isBusy)
            return;
        
        if (TryHandleUnitSelection()) 
            return;

        HandleSelectedAction();

    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }
        }
    }

    // public void HandleSelectedAction() //one way - no abstract func so we need to handle every case bec we need diff prams for the actions
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
    //         
    //         switch (selectedAction)
    //         {
    //             case MoveAction moveAction:
    //                 if (moveAction.IsValidActionGridPosition(mouseGridPosition))
    //                 {
    //                     SetBusy();
    //                     moveAction.Move(mouseGridPosition, ClearBusy);
    //                 }
    //                 break;
    //             
    //             case SpinAction spinAction:
    //                 SetBusy();
    //                 spinAction.Spin(ClearBusy);
    //                 break;
    //         }
    //     }
    // }
    
    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }
    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue,unitLayer))
            {
                if (raycastHit.transform.TryGetComponent(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        
        SetSelectedAction(unit.GetMoveAction());
        
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    { 
        selectedAction = baseAction;
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
