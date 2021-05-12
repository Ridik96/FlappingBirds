using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button pauseMenuButton;
    public TMPro.TextMeshProUGUI pointsText;
    [HideInInspector] public bool buttons;

    public bool SetPause
    {
        set
        {
            buttons = value;
            pauseMenuButton.interactable = buttons;
            pointsText.enabled = buttons;
        }
    }
    
    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }

    public void Start()
    {
        pauseMenuButton.onClick.AddListener(delegate { GameplayManager.Instance.PlayPause(); });
    }
   
}
