using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultKnife : Knife
{
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _hitEffect = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        Vibration.Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vibration.Vibrate();
        CheckCollision(collision);
    }
}
