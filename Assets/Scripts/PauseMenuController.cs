using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenuController : MonoBehaviour
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
        restartButton.onClick.AddListener(delegate { OnRestart(GameplayManager.Instance.GameDatabase.sceneName[1]); });
        beckToMenu.onClick.AddListener(delegate { OnQuit(GameplayManager.Instance.GameDatabase.sceneName[0]); });
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

    public void OnRestart(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnQuit(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePaused -= OnPause;
    }

}
    

