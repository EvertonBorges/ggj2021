using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _keyPrefab = null;
    [SerializeField] private Transform[] _spawnPositions = null;

    void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        var index = Random.Range(0, _spawnPositions.Length);
        var spawnTransform = _spawnPositions[index];
        Instantiate(_keyPrefab, spawnTransform.position, _keyPrefab.transform.rotation, spawnTransform);
    }

}
