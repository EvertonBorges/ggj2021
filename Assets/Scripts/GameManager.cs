using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private KeyScriptable _roomKeyScriptable = null;
    [SerializeField] private KeyScriptable _houseKeyScriptable = null;
    
    [SerializeField] private GameObject _keyPrefab = null;

    private GameObject[] _spawnPositionsRoomKey = null;
    private GameObject[] _spawnPositionsHouseKey = null;
    // private GameObject[] _spawnPositionsTestKey = null;

    protected override void Init()
    {
        _spawnPositionsRoomKey = GameObject.FindGameObjectsWithTag(Tags.SpawnRoomKey.ToString());
        _spawnPositionsHouseKey = GameObject.FindGameObjectsWithTag(Tags.SpawnHouseKey.ToString());
        // _spawnPositionsTestKey = GameObject.FindGameObjectsWithTag(Tags.SpawnTestKey.ToString());

        Setup();
    }

    private void Setup()
    {
        var indexRoomKey = Random.Range(0, _spawnPositionsRoomKey.Length);
        var spawnRoomTransform = _spawnPositionsRoomKey[indexRoomKey].transform;
        var roomKey = Instantiate(_keyPrefab, spawnRoomTransform.position, _keyPrefab.transform.rotation, spawnRoomTransform).GetComponent<Key>();
        roomKey.Setup(_roomKeyScriptable);

        var indexHouseKey = Random.Range(0, _spawnPositionsHouseKey.Length);
        var spawnHouseTransform = _spawnPositionsHouseKey[indexHouseKey].transform;
        var houseKey = Instantiate(_keyPrefab, spawnHouseTransform.position, _keyPrefab.transform.rotation, spawnHouseTransform).GetComponent<Key>();
        houseKey.Setup(_houseKeyScriptable);

        // var indexTestKey = Random.Range(0, _spawnPositionsTestKey.Length);
        // var spawnTestTransform = _spawnPositionsTestKey[indexTestKey].transform;
        // var testKey = Instantiate(_keyPrefab, spawnTestTransform.position, _keyPrefab.transform.rotation, spawnTestTransform).GetComponent<Key>();
        // testKey.Setup(_houseKeyScriptable);
    }

}
