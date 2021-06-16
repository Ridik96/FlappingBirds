
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    //Private
    private EGameState m_state;
    private int m_points = 0;

    //Class
    public GameSettingsDatabase GameDatabase;
    public BirdController mainHero;
    public TerreinObjectPool TerreinObjectPool;
    public ObstacleObjectPool ObstacleObjectPool;
    public HUDController m_HUD;

    //Structs
    public int terrainPoolCount;
    public int ObstaclePoolCount;
    public float terrainWidth;
    public float emptySpace;
    [HideInInspector] public GameObject terrein;
    [HideInInspector] public GameObject Obstacle;
    //[HideInInspector] public bool spawnedObstacle;

    //Delegate and Event
    public delegate void GameStateCallback();
    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;

    public EGameState GameState
    {
        get => m_state;
        set
        {
            m_state = value;
            switch (m_state)
            {
                case EGameState.Paused:
                    {
                        OnGamePaused?.Invoke();
                        m_HUD.SetPause = false;
                    }
                    break;
                case EGameState.Playing:
                    {
                        OnGamePlaying?.Invoke();
                        m_HUD.SetPause = true;
                    }
                    break;
            }
        }
    }

    public int Points
    {
        get { return m_points; }
        set
        {
            if (m_points != value)
            {
                m_points = value;
                m_HUD.UpdatePoints(m_points);
            }
        }
    }

    void Start()
    {
        TerreinObjectPool.Init(GameDatabase.treenRef, terrainPoolCount);
        ObstacleObjectPool.Init(GameDatabase.obstacleRef, ObstaclePoolCount);
        TerreinObjectPool.SetObject(new Vector3(mainHero.transform.position.x, 0, 0), Quaternion.identity);
        ObstacleObjectPool.SetObject(new Vector3(17, 0, 0), Quaternion.identity);
        ObstacleObjectPool.SetObject(new Vector3(17+emptySpace, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        //SpawnedTerrain
        if (mainHero.transform.position.x - terrein.transform.position.x > 0.0f)
        {
            TerreinObjectPool.SetObject(new Vector3(mainHero.transform.position.x + (terrainWidth - 0.2f), 0, 0), Quaternion.identity);
        }
      
       /* //SpawnedObstacle
        if (mainHero.transform.position.x > (Obstacle.transform.position.x ))
       if(spawnedObstacle)
        {
            ObstacleObjectPool.SetObject(new Vector3((Obstacle.transform.position.x  + emptySpace ), 0, 0), Quaternion.identity);
        }*/


        if (Input.GetKey(KeyCode.Escape))
            GameState = EGameState.Paused;
       }

    public void PlayPause()
    {
        switch (GameState)
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