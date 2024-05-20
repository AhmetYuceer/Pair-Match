using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<SOGameSettings> AllLevels = new List<SOGameSettings>();
    public SOGameSettings SOGameSettings { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void StartLevel(int level)
    {
        if (!AllLevels[level].Locked)
        {
            SOGameSettings = AllLevels[level];
            SceneManager.LoadScene(1);
        }
    }

    public void LevelComplated(SOGameSettings level)
    {
        var result = CheckLevel(level);
        if (result.Item1)
            StartLevel(result.Item2);
        else
            SceneManager.LoadScene(0);
    }

    public (bool , int)CheckLevel(SOGameSettings level)
    {
        int levelIndex = AllLevels.IndexOf(level);
        levelIndex++;

        if (levelIndex < AllLevels.Count)
        {
            AllLevels[levelIndex].Locked = false;
            return (true ,levelIndex);
        }
        return (false, levelIndex);
    }

    public List<SOGameSettings> GetAllLevels()
    {
        return AllLevels;
    }
}