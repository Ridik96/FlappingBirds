
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScensLoader : MonoBehaviour
{
    public Button loadedScenesButton;
    public string sceneName;


    void Start()
    {
        loadedScenesButton.onClick.AddListener(delegate { OnloadScenes(); });
    }

    private void OnloadScenes()
    {
        SceneManager.LoadScene(sceneName);
    }



}

