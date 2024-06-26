using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;
    
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
        
        if(!TurnSystem.Instance.IsPlayerTurn())
            return;
        
        if(EventSystem.current.IsPointerOverGameObject()) //if the mouse is on UI element
            return;
        
        if (TryHandleUnitSelection()) 
            return;

        HandleSelectedAction();

    }

    private void HandleSelectedAction()
    {
        if (InputManager.Instance.IsMouseButtonDownThisFrame())
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
                return;
            
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
                return;
            
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            
            OnActionStarted?.Invoke(this, EventArgs.Empty);
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
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }
    private bool TryHandleUnitSelection()
    {
        if (InputManager.Instance.IsMouseButtonDownThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue,unitLayer))
            {
                if (raycastHit.transform.TryGetComponent(out Unit unit))
                {
                    if (unit == selectedUnit)
                        return false;
                    
                    if(unit.IsEnemy())
                        return false;
                    
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
        
        SetSelectedAction(unit.GetAction<MoveAction>());
        
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    { 
        selectedAction = baseAction;
        
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
