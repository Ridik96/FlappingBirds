using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsController : MonoBehaviour
{
    //Button
    public Button acceptButton;
    public Button cancelButton;

    public MainMenuController mainMenu;
    
    private float m_initialVolume = 0.0f;

    void Start()
    {
        acceptButton.onClick.AddListener(delegate { OnAccept(); });
        cancelButton.onClick.AddListener(delegate { OnCancel(); });
    }

    private void OnEnable()
    {
        m_initialVolume = AudioListener.volume;

    }

    private void OnAccept()
    {
        SaveManager.Instance.SaveData.m_masterVolume = AudioListener.volume;
        SaveManager.Instance.SaveSettings();
        mainMenu.ShowOptions(false);
    }

    private void OnCancel()
    {
        AudioListener.volume = m_initialVolume;
        mainMenu.ShowOptions(false);
    }

    
}
