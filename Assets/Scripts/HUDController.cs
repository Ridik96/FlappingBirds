using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button pauseMenuButton;
    public TMPro.TextMeshProUGUI PointsText;
    
    public void UpdatePoints(int points)
    {
        PointsText.text = "Points: " + points;
    }

    public void Start()
    {
        pauseMenuButton.onClick.AddListener(delegate { GameplayManager.Instance.PlayPause(); });
    }
   
}
