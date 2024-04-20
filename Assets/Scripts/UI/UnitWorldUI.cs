using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit Unit;


    private void Start()
    {
        Unit.OnAnyActionPointsChanged += unit_OnAnyActionPointsChanged; //update every UnitWorldUI cuz the event is static, can be problem for 1000 units but for now its ok
        
        UpdateActionPointsText();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = Unit.GetActionPoints().ToString();
    }

    private void unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }
}
