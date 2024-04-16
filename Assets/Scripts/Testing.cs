using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem _gridSystem;
    private void Start()
    {
        _gridSystem = new GridSystem(10, 10, 2f);
        
        Debug.Log(new GridPosition(5, 7));
    }

    private void Update()
    {
        Debug.Log(_gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}
