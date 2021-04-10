using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button resumeButton;
    public Button restartButton;
    public Button beckToMenu;

   [Header("Panel")]
    public GameObject pausePanel;

    void Start()
    {
        resumeButton.onClick.AddListener(delegate { OnResume(); });
        restartButton.onClick.AddListener(delegate { OnRestart(); });
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

    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    

