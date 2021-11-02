using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [HideInInspector] public bool buttons;
    [HideInInspector] public bool endGame;
    public TMPro.TextMeshProUGUI pointsText;
    public Button pauseMenuButton;
    public Button restartButton;
    public Button beckToMenu;

    public GameObject endGamePanel;
    public PauseMenuController pauseMenu;

    public bool SetPause
    {
        set
        {
            buttons = value;
            pauseMenuButton.interactable = buttons;
            pointsText.enabled = buttons;
        }
    }
    public bool SetEnd
    {
        set 
        { 
            endGame = value;
            pauseMenuButton.interactable = !endGame;
            endGamePanel.SetActive(endGame);
        }
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }

    public void Start()
    {
        pauseMenuButton.onClick.AddListener(delegate { GameplayManager.Instance.PlayPause(); });
        restartButton.onClick.AddListener(delegate { pauseMenu.OnRestart(GameplayManager.Instance.GameDatabase.sceneName[1]); });
        beckToMenu.onClick.AddListener(delegate { pauseMenu.OnRestart(GameplayManager.Instance.GameDatabase.sceneName[0]); });
        endGamePanel.SetActive(false);
    }
    public void SetPanelVisible(bool visible) => endGamePanel.SetActive(visible);
}
