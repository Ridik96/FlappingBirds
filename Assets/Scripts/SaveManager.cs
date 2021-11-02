using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : Singleton<SaveManager>

{
    private string m_pathBin;
    private string m_pathJSON;
    private float m_overallTime = 0.0f;

    public GameSaveData SaveData;
    public bool useBinary = true;

    public void SaveSettings() 
    {
        m_overallTime += SaveData.m_timeSinceLastSave;
        Debug.Log("Saving overall time value: " + m_overallTime);
        PlayerPrefs.SetFloat("OverallTime", m_overallTime);
        SaveData.m_timeSinceLastSave = 0.0f;

        if (useBinary)
        {
            FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, SaveData);
            file.Close();
        }
        else
        {
            string saveData = JsonUtility.ToJson(SaveData);
            File.WriteAllText(m_pathJSON, saveData);
        }
    }

    public void LoadSettings()
    {
        m_overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);
        Debug.Log("Loaded overall time value: " + m_overallTime);

        if (useBinary && File.Exists(m_pathBin))
        {
            FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            SaveData = (GameSaveData)binFormat.Deserialize(file);
            ApplySettings();
            file.Close();
        }
        else if (!useBinary && File.Exists(m_pathJSON))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            SaveData = JsonUtility.FromJson<GameSaveData>(saveData);
            ApplySettings();
        }
        else
        {
            SaveData.m_timeSinceLastSave = 0.0f;
            SaveData.m_masterVolume = AudioListener.volume;
        }
    }

    void Start()
    {
        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");
        SaveData.m_timeSinceLastSave = 0.0f;
        SaveData.m_masterVolume = AudioListener.volume;
        LoadSettings();
    }

    void Update()
    {
        SaveData.m_timeSinceLastSave = Time.deltaTime;

    }
    public void ApplySettings()
    {
        AudioListener.volume = SaveData.m_masterVolume;
    }
}
public struct GameSaveData
{
    public float m_timeSinceLastSave;
    public float m_masterVolume;
}