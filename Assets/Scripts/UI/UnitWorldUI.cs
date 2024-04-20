using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit Unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;


    private void Start()
    {
        Unit.OnAnyActionPointsChanged += unit_OnAnyActionPointsChanged; //update every UnitWorldUI cuz the event is static, can be problem for 1000 units but for now its ok

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        
        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = Unit.GetActionPoints().ToString();
    }

    private void unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
    
    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }
}
