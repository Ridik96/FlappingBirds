
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private BirdController mainHero;
    private List<GameObject> spawnedTerrain = new List<GameObject>();
    private List<GameObject> spawnedObstacle = new List<GameObject>();
    private EGameState m_state;
    private int m_points = 0 ;

    public float terrainWidth;
    public float emptySpace;
    public GameSettingsDatabase GameDatabase;
    public HUDController m_HUD;
    public delegate void GameStateCallback();
    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;

    public EGameState GameState
    {
        get => m_state;
        set
        {
            m_state = value;
            switch(m_state)
            {
                case EGameState.Paused:
                    {
                        OnGamePaused?.Invoke();
                    }
                    break;
                case EGameState.Playing:
                    {
                        OnGamePlaying?.Invoke();
                    }
                    break;
            }

        }
    }
    void Start()
    {
        Points = 0;
        mainHero = FindObjectOfType<BirdController>();
        spawnedTerrain.Add(Instantiate(GameDatabase.treenRef, new Vector3(mainHero.transform.position.x , 0, 0), Quaternion.identity));
        spawnedObstacle.Add(Instantiate(GameDatabase.obstacleRef, new Vector3(17, 0, 0), Quaternion.identity));
        spawnedObstacle.Add(Instantiate(GameDatabase.obstacleRef, new Vector3(spawnedObstacle[spawnedObstacle.Count - 1].transform.position.x + emptySpace, 0, 0), Quaternion.identity));

    }

    void Update()
    {
        //SpawnedTerrain
        if (mainHero.transform.position.x - spawnedTerrain[spawnedTerrain.Count - 1].transform.position.x > 0.0f)
        {
            spawnedTerrain.Add(Instantiate(GameDatabase.treenRef, new Vector3(mainHero.transform.position.x + terrainWidth * 0.5f, 0, 0), Quaternion.identity));
        }
        if ((spawnedTerrain.Count > 0) && (mainHero.transform.position.x - spawnedTerrain[0].transform.position.x > terrainWidth))
        {
            GameObject.Destroy(spawnedTerrain[0]);
            spawnedTerrain.RemoveAt(0);
        }

        //SpawnedObstacle
        if (mainHero.transform.position.x > spawnedObstacle[spawnedObstacle.Count - 2].transform.position.x)
        {
            spawnedObstacle.Add(Instantiate(GameDatabase.obstacleRef, new Vector3(spawnedObstacle[spawnedObstacle.Count - 1].transform.position.x + emptySpace, 0, 0), Quaternion.identity));
        }
        if ((spawnedObstacle.Count > 0) && (mainHero.transform.position.x - spawnedObstacle[0].transform.position.x > emptySpace * 2))
        {
            GameObject.Destroy(spawnedObstacle[0]);
            spawnedObstacle.RemoveAt(0);
        }

        if (Input.GetKey(KeyCode.Escape))
            GameState = EGameState.Paused;
    }
    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
        }
    }

    public void PlayPause()
    {
        switch(GameState)
        {
            case EGameState.Paused:
                {
                    GameState = EGameState.Playing;    
                }
                break;
            case EGameState.Playing:
                {
                    GameState = EGameState.Paused;
                }
                break;
        }
    }
    public enum EGameState
    {
        Playing,
        Paused
    }
}

