using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;
    public static GameManager Instance
    {
        get
        {
            if (_gameManager == null)
            {
                _gameManager = FindObjectOfType<GameManager>();
            }
            return _gameManager;
        }
    }

    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private UIManager uiManager;

    private bool isGameEnded=false;
    private int currentHits;

    public int EnemyKnivesCount { get; private set; }
    public int ScoreToWin { get; private set; }

    public event UnityAction<int> NewTargetSpawned;

    private void OnEnable()
    {
        Knife.KnifeHitted += EndGame;
        Knife.TargetHitted += CheckHits;
    }

    private void OnDisable()
    {
        Knife.KnifeHitted -= EndGame;
        Knife.TargetHitted -= CheckHits;
    }

    private void Awake()
    {
        StartCoroutine(InstantiateTarget());
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        Debug.Log(Time.timeScale);
    }

    IEnumerator InstantiateTarget() 
    {
        yield return new WaitForSeconds(1f);

        ScoreToWin = Random.Range(3, 9);
        EnemyKnivesCount = Random.Range(1, 3);
        NewTargetSpawned?.Invoke(ScoreToWin);
        Instantiate(_targetPrefab, new Vector3(0, 0.2f, 2.5f),Quaternion.Euler(180,0,0));

        currentHits = 0;
    }

    private void CheckHits() 
    {
        currentHits++;
        if (currentHits >= ScoreToWin) 
        {
            StartCoroutine(InstantiateTarget());
        }
    }

    private void EndGame(bool gameStatus) 
    {
        isGameEnded = gameStatus;
        StartCoroutine(StopSession());
    }

    IEnumerator StopSession() 
    {
        yield return new WaitForSeconds(1f);
        uiManager.SwitchWindows(WindowType.LoseWindow);
    }

    
}
