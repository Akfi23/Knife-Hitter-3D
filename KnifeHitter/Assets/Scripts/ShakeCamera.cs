using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Shaker shaker;
    [SerializeField] private ShakePreset shakePreset;

    private void OnEnable()
    {
        Knife.TargetHitted += ShakeByHit;
    }

    private void OnDisable()
    {
        Knife.TargetHitted -= ShakeByHit;
    }
       
    private void ShakeByHit() 
    {
        shaker.Shake(shakePreset);
    }
}
