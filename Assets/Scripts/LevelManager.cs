using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "LevelManager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] private int mainMenuBuildIndex = 0;
    [SerializeField] private int firstLevelBuildIndex = 1;
    
    public delegate void OnLevelFinished();
    public static event OnLevelFinished onLevelFinished;

    public void GoToMainMenu()
    {
        LoadSceneByIndex(mainMenuBuildIndex);
    }

    private void LoadSceneByIndex(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        UIManager.SetGamePaused(false);
    }
    
    public void LoadFirstLevel()
    {
        LoadSceneByIndex(firstLevelBuildIndex);
    }
    
    public void RestartCurrentLevel()
    {
        LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void LevelFinished()
    {
        onLevelFinished?.Invoke();
    }
}
