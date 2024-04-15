using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataController : MonoBehaviour
{
    private static DataController instance { get; set; }
    [SerializeField] private HighScoreData _Data;

    private const string FILE_NAME = "HighScoreData";


    public bool Defeat = false;
    public int KillsHighScore;
    public int Kills;
    public int Combo;
    public int DifficultyVal;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            CheckData();
            LoadData();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void LoadData()
    {
        KillsHighScore = _Data.killsHighScore;
    }
    public void ReWriteData()
    {
        _Data.killsHighScore = KillsHighScore;
    }
    private void CheckData()
    {
        _Data = SaveData.Load<HighScoreData>(FILE_NAME);
        if (_Data == default)
        {
            _Data = new HighScoreData();
            ReWriteData();
        }
    }
    private void OnApplicationQuit() 
    {
        ReWriteData();
        SaveData.Save(_Data,FILE_NAME);
    }
}