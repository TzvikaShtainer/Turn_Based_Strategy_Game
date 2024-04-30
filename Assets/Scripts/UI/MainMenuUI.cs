using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        startBtn.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        levelManager.LoadFirstLevel();
    }
}
