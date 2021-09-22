using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Thrower : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _knifes = new List<Rigidbody>();
    [SerializeField] private Rigidbody _knifePrefab;
    [SerializeField] private float _force;
    [SerializeField] private GameObject _aimCursor;
    [SerializeField] private float _ThrowDelay;
    private float _currentTimer = 0;

    public event UnityAction<int> KnifesStartCount;

    private void OnEnable()
    {
        GameManager.Instance.NewTargetSpawned += InitializeKnifes;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.NewTargetSpawned -= InitializeKnifes;
    }
    
    private void Start()
    {
        InitializeKnifes(GameManager.Instance.ScoreToWin);
        KnifesStartCount?.Invoke(_knifes.Count);
    }

    private void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= _ThrowDelay)
            _aimCursor.SetActive(true);
    }

    private void InitializeKnifes(int count) 
    {
        for (int i = 0; i < count; i++)
        {
            Rigidbody newKnife = Instantiate(_knifePrefab, gameObject.transform);
            newKnife.gameObject.SetActive(false);
            _knifes.Add(newKnife);
        }
    }

    public void TryToThrow() 
    {
        if (_currentTimer >= _ThrowDelay)
        {
            if (TryGetKnife(out Rigidbody knife))
            {
                _currentTimer = 0;
                LaunchKnife(knife);
            }
        }
    }

    private bool TryGetKnife(out Rigidbody result)
    {
        result = _knifes.First(knife => knife.gameObject.activeSelf == false);
        return result != null;
    }

    private void LaunchKnife(Rigidbody knife)
    {
        _aimCursor.SetActive(false);

        knife.gameObject.SetActive(true);
        knife.AddForce(new Vector3(0,_force, 0), ForceMode.Impulse);
        _knifes.Remove(knife);
    }
}
