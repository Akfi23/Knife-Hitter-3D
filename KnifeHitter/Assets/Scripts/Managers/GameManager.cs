using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LoseWIndow loseWindow;
    [SerializeField] private ChanceData data;

    public bool isSpawned { get; private set; }

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

    public IEnumerator InstantiateTarget() 
    {
        yield return new WaitForSeconds(1f);

        ScoreToWin = Random.Range(3, 9);
        EnemyKnivesCount = Random.Range(1, 4);
        NewTargetSpawned?.Invoke(ScoreToWin);
        isSpawned = data.GetChance();
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
        StartCoroutine(StopSession());
    }

    IEnumerator StopSession() 
    {
        yield return new WaitForSeconds(1f);
        uiManager.SwitchWindows(WindowType.LoseWindow);
    }

    public void InstantiateOnReaload() 
    {
        StartCoroutine(InstantiateTarget());
    }
    
}
