using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    [Header("Panel")]
    public GameObject mainPanel;
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    void Start()
    {
        playButton.onClick.AddListener(delegate { OnPlay(); });
        optionsButton.onClick.AddListener(delegate { ShowOptions(true); });
        exitButton.onClick.AddListener(delegate { OnQuit(); });
       
    }

    public void SetPanelVisible(bool visible) => mainPanel.SetActive(visible);

    public void OnPlay()
    {
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        SetPanelVisible(false);
    }

    public void ShowOptions(bool bShow)
    {
        optionsPanel.SetActive(bShow);
        mainMenuPanel.SetActive(!bShow);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
