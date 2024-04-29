using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager: MonoBehaviour
    {
        public static UIManager Instance;
        
        [SerializeField] private CanvasGroup pausedMenu;
        [SerializeField] private CanvasGroup inGamePlayUI;
        
        [SerializeField] private Button pauseBtn;
        
        
        
        private CanvasGroup currActiveGroup;
        
        private List<CanvasGroup> allChildern = new List<CanvasGroup>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            List<CanvasGroup> children = new List<CanvasGroup>();
            GetComponentsInChildren(true, children);
            foreach (CanvasGroup child in children)
            {
                if (child.transform.parent == transform)
                {
                    allChildern.Add(child);
                    SetGroupActive(child, false, false);
                }
            }

            if (allChildern.Count != 0)
            {
                SetCurrentActiveGroup(allChildern[0]);
            }
            
            
            pauseBtn.onClick.AddListener(SwitchToPausedMenu);
        }

        private void SetCurrentActiveGroup(CanvasGroup canvasGroup)
        {
            if (currActiveGroup != null)
            {
                SetGroupActive(currActiveGroup, false, false);
            }

            currActiveGroup = canvasGroup;
            SetGroupActive(currActiveGroup, true, true);
        }
        
        private void SetGroupActive(CanvasGroup child, bool interactable, bool visible)
        {
            child.interactable = interactable;
            child.blocksRaycasts = interactable;
            child.alpha = visible ? 1 : 0;
        }
        
        public void SwitchToPausedMenu()
        {
            SetCurrentActiveGroup(pausedMenu);
            SetGamePaused(true);
        }
        
        public void SwitchToInGamePlayUI()
        {
            SetCurrentActiveGroup(inGamePlayUI);
            SetGamePaused(false);
        }
        
        public static void SetGamePaused(bool paused)
        {
            Time.timeScale = paused ? 0 : 1;
        }
        
    }
}