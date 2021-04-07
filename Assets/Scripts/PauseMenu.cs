using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button resumeButton;
    public Button beckToMenu;

   [Header("Panel")]
    public GameObject pausePanel;

    void Start()
    {
        resumeButton.onClick.AddListener(delegate { OnResume(); });
        beckToMenu.onClick.AddListener(delegate { OnQuit(); });
        pausePanel.SetActive(false);
        GameplayManager.OnGamePaused += OnPause;
    }

    public void SetPanelVisible(bool visible) => pausePanel.SetActive(visible);

    private void OnPause() => SetPanelVisible(true);

    private void OnResume()
    {
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        pausePanel.SetActive(false);
        
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePaused -= OnPause;
    }

}
    

