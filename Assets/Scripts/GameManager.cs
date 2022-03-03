using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private KeyScriptable _roomKeyScriptable = null;
    [SerializeField] private KeyScriptable _houseKeyScriptable = null;
    
    [SerializeField] private GameObject _keyPrefab = null;

    [SerializeField] private FadeEffect _uiBg = null;

    private GameObject[] _spawnPositionsRoomKey = null;
    private GameObject[] _spawnPositionsHouseKey = null;

    private float _startTime = 0f;
    public float StartTime => _startTime;

    protected override void Init()
    {
        _spawnPositionsRoomKey = GameObject.FindGameObjectsWithTag(Tags.SpawnRoomKey.ToString());
        _spawnPositionsHouseKey = GameObject.FindGameObjectsWithTag(Tags.SpawnHouseKey.ToString());
    }

    protected override void StartInit()
    {
        Setup();
    }

    private void Setup()
    {
        FirstPersonMovement.Instance?.SetBlockMovement(true);

        var indexRoomKey = Random.Range(0, _spawnPositionsRoomKey.Length);
        var spawnRoomTransform = _spawnPositionsRoomKey[indexRoomKey].transform;
        var roomKey = Instantiate(_keyPrefab, spawnRoomTransform.position, _keyPrefab.transform.rotation, spawnRoomTransform).GetComponentInChildren<Key>();
        roomKey.Setup(_roomKeyScriptable);

        var indexHouseKey = Random.Range(0, _spawnPositionsHouseKey.Length);
        var spawnHouseTransform = _spawnPositionsHouseKey[indexHouseKey].transform;
        var houseKey = Instantiate(_keyPrefab, spawnHouseTransform.position, _keyPrefab.transform.rotation, spawnHouseTransform).GetComponentInChildren<Key>();
        houseKey.Setup(_houseKeyScriptable);

        _uiBg.Effect(false, () => {
            FirstPersonMovement.Instance?.SetBlockMovement(false);
            
            _startTime = Time.time;
        });
    }

}
