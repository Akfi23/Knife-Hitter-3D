using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/Chnace Data",fileName ="Chance")]
public class ChanceData : ScriptableObject
{
    [SerializeField] private float _chanceToSpawn;
    private float _randomValue;
    public bool GetChance() 
    {
        _randomValue = Random.value;

        if (_chanceToSpawn > _randomValue) 
        {
            return true;
        }

        return false;
    }
}
