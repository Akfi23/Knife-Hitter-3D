using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private static ScoreCounter _scoreCounter;
    public static ScoreCounter Instance
    {
        get
        {
            if (_scoreCounter == null)
            {
                _scoreCounter = FindObjectOfType<ScoreCounter>();
            }
            return _scoreCounter;
        }
    }

    private int currentScore;
    public int AppleHits { get; private set; }
    public int RecordLvl { get; private set; }

    public event UnityAction<int> OnAppleScored;


    private void OnEnable()
    {
        Knife.AppleHitted += AddAppleScore;
        GameManager.Instance.NewTargetSpawned += AddNewRecord;
    }

    private void OnDisable()
    {        
        Knife.AppleHitted -= AddAppleScore;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.NewTargetSpawned -= AddNewRecord;
    }

    private void Awake()
    {
        RecordLvl = GetRecords();
        AppleHits = GetApples();
    }

    private void AddAppleScore() 
    {
        AppleHits++;
        OnAppleScored?.Invoke(AppleHits);
        SetApples();
    }
   
    private void AddNewRecord(int count) 
    {
        currentScore++;
        if (currentScore >= RecordLvl) 
        {
            RecordLvl = currentScore;
            SetRecords();
        }
    }
    
    private void SetApples() 
    {
        PlayerPrefs.SetInt("apple", AppleHits);
    }

    private void SetRecords() 
    {
        PlayerPrefs.SetInt("record", RecordLvl);
    }

    private int GetApples() 
    {
        return PlayerPrefs.GetInt("apple");
    }

    private int GetRecords() 
    {
        return PlayerPrefs.GetInt("record");
    }
}
