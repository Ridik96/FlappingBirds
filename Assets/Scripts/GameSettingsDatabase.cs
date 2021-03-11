using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings",menuName = "ScriptableObject/Create Game settings", order = 1)]
public class GameSettingsDatabase : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject treenRef;
    public GameObject obstacleRef;

    [Header("AudioClip")]
    public AudioClip Jump;
    public AudioClip Power;
    public AudioClip Inpact;
}
