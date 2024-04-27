using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubesPool))]

public class CubeRain : MonoBehaviour
{
    [SerializeField] private Transform _platformTransform;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private float _spawnFrequency = 10f;

    private CubesPool _cubesPool;

    private Coroutine _cubesSpawn;

    private float _minSpawnPositionX;
    private float _maxSpawnPositionX;
    private float _minSpawnPositionZ;
    private float _maxSpawnPositionZ;

    private bool _isRaining = true;

    private void Awake()
    {
        SetPosition();
        SetSpawnLimits();
    }

    private void Start()
    {
        _cubesPool = GetComponent<CubesPool>();
        _cubesSpawn = StartCoroutine(SpawnCubes());  
    }

    private void OnDisable()
    {
        if (_cubesSpawn != null)
            StopCoroutine(_cubesSpawn);
    }
    private void SetSpawnLimits()
    {
        int halfDevider = 2;
        float offset = 2f;

        _minSpawnPositionX = transform.position.x - _platformTransform.localScale.x / halfDevider + offset;
        _maxSpawnPositionX = transform.position.x + _platformTransform.localScale.x / halfDevider - offset;
        _minSpawnPositionZ = transform.position.z - _platformTransform.localScale.z / halfDevider + offset;
        _maxSpawnPositionZ = transform.position.z + _platformTransform.localScale.z / halfDevider - offset;
    }

    private void SetPosition()
    {
        Vector3 position = _platformTransform.position;
        position.y += _spawnHeight;
        transform.position = position;
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomPositionX = Random.Range( _minSpawnPositionX, _maxSpawnPositionX);
        float randomPositionZ = Random.Range( _minSpawnPositionZ, _maxSpawnPositionZ);
        
        return new Vector3(randomPositionX, transform.position.y, randomPositionZ);
    }

    private IEnumerator SpawnCubes()
    {
        float delayInseconds = 1 / _spawnFrequency;
        WaitForSeconds delay = new WaitForSeconds(delayInseconds);

        while (_isRaining)
        {
            if (_cubesPool.TryGetCube(out Cube newCube))
                newCube.transform.position = GenerateRandomPosition();

            yield return delay;
        }
    }
}
