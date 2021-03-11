using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    //Buttons
    public Button resumeButton;
    public Button beckToMenu;
    //Panel
    public GameObject pausePanel;

    void Start()
    {
        resumeButton.onClick.AddListener(delegate { onResume(); });
        beckToMenu.onClick.AddListener(delegate { onQuit(); });
        pausePanel.SetActive(false);
        GameplayManager.OnGamePaused += onPause;
    }

    public void SetPanelVisible(bool visible) => pausePanel.SetActive(visible);

    private void onPause() => SetPanelVisible(true);

    private void onResume()
    {
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        pausePanel.SetActive(false);
        
    }

    private void onQuit()
    {

    }



}
    

