using System.Collections.Generic;
using UnityEngine;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _maxCubesCount;

    private List<Cube> _cubes;

    private void Awake()
    {
        _cubes = new List<Cube>(_maxCubesCount);

        for (int i = 0; i < _maxCubesCount; i++)
        {
            _cubes.Add(Instantiate(_cubePrefab));
            _cubes[i].gameObject.SetActive(false);
        }
    }

    public bool TryGetCube(out Cube newCube)
    {
        newCube = null;

        foreach (Cube cube in _cubes) 
        {
            if (cube.gameObject.activeSelf == false)
            {
                newCube = cube;
                cube.gameObject.SetActive(true);
                return true;
            }
        }

        return false;
    }
}
