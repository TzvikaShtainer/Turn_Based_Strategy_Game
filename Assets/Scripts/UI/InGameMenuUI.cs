using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button mainMenuBtn;
    
    
    private void Start()
    {
        resumeBtn.onClick.AddListener(ResumeGame);
        restartBtn.onClick.AddListener(RestartGame);
        mainMenuBtn.onClick.AddListener(ReturnToMainMenu);
    }

    private void ResumeGame()
    {
        Debug.Log("ResumeGame");
        UIManager.Instance.SwitchToInGamePlayUI();
    }
    
    private void RestartGame()
    {
        Debug.Log("RestartGame");
    }

    private void ReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu");
    }
}
