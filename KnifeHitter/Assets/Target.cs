using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class Target : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _explodeEffect;
    [SerializeField] private int _health;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Transform _enemyKnifePrefab;
    [SerializeField] private Transform _applePrefab;

    private void OnDisable()
    {
        Knife.TargetHitted -= SubtrackHP;
    }

    private void Start()
    {
        Knife.TargetHitted += SubtrackHP;

        _transform = GetComponent<Transform>();
        _health = GameManager.Instance.ScoreToWin;
        _sphereCollider = GetComponent<SphereCollider>();

        SpawnEnemyKnives(GameManager.Instance.EnemyKnivesCount);
        SpawnAplle();
        Rotate();

    }

    private void Update()
    {
    }

    private void Rotate()
    {
        //_transform.Rotate(0, 0, _speed * Time.deltaTime);
        Tween tween = _transform.DORotate(new Vector3(0, 0, _speed),10,RotateMode.WorldAxisAdd).SetLoops(10,LoopType.Yoyo).SetDelay(1f);
    }

    private void Explode()
    {
        DOTween.Clear();
        Instantiate(_explodeEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void SubtrackHP()
    {
        _health--;
        if (_health == 0)
        {
            Explode();
        }
    }

    private void SpawnEnemyKnives(int knivesCount)
    {
        float angleStep = 360 / knivesCount;

        for (int i = 0; i < knivesCount; i++)
        {
            Transform newObjectTransform = Instantiate(_enemyKnifePrefab, _transform);

            SetPositionAndRotation(newObjectTransform,angleStep,i);
        }
    }

    private void SpawnAplle() 
    {
        Transform apple= Instantiate(_applePrefab,_transform);
        SetPositionAndRotation(apple);
    }
    
    private void SetPositionAndRotation(Transform prefab,float angle,int offset) 
    {
        prefab.localPosition= new Vector3((_sphereCollider.radius+ 0.003f) * Mathf.Cos(angle * (offset + 1) * Mathf.Deg2Rad),
                (_sphereCollider.radius + 0.003f) * Mathf.Sin(angle * (offset + 1) * Mathf.Deg2Rad), 0);

        prefab.localRotation= Quaternion.Euler(0, 0, angle + (offset * angle + -90));
    }

    private void SetPositionAndRotation(Transform prefab)
    {
        prefab.localPosition = new Vector3((_sphereCollider.radius + 0.001f) * Mathf.Cos(90 * (4 + 1) * Mathf.Deg2Rad),
                (_sphereCollider.radius + 0.001f) * Mathf.Sin(90 * (4+1) * Mathf.Deg2Rad), 0);

        prefab.localRotation = Quaternion.Euler(0, 0, 360);
    }
}


