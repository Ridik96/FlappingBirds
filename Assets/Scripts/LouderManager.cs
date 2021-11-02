using System.Collections.Generic;
using UnityEngine;

public class LouderManager : MonoBehaviour
{
    public List<GameObject> manager = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < manager.Count; i++)
        {
            DontDestroyOnLoad(manager[i]);
        }
    }
}
