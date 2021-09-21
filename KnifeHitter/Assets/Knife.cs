using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Knife : MonoBehaviour
{
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected ParticleSystem _hitEffect;
    [SerializeField] protected AudioSource _audioSource;
    protected bool _isKnifeHitted = false;

    public static event UnityAction<bool> KnifeHitted;
    public static event UnityAction TargetHitted;
    public static event UnityAction AppleHitted;


    protected void CheckCollision(Collision collision) 
    {
        if (collision.gameObject.TryGetComponent<Target>(out Target target))
        {
            TargetHitted?.Invoke();
            _audioSource.Play();
            _transform.parent = target.transform;
            _rigidbody.isKinematic = true;
            _hitEffect.Play();
        }
        else if (collision.gameObject.TryGetComponent<Apple>(out Apple apple)) 
        {
            AppleHitted?.Invoke();
            Destroy(apple.gameObject);
        }
        else
        {
            _isKnifeHitted = true;

            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -2, _rigidbody.velocity.z);

            KnifeHitted?.Invoke(_isKnifeHitted);
        }
    }
}
