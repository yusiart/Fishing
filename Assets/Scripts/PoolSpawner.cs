using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
  [SerializeField] private GameObject _poolPrefab;
  [SerializeField] private GameObject[] _fishes;
  [SerializeField] private Transform _poolGenerationPoint;
  [SerializeField] private List<GameObject> _pools;

  private float _spaceBetween = 30f;
  private float _deepth = 30f;

  private void Awake()
  {
      for (int i = 0; i < _fishes.Length - 1; i++)
      {
          var currentPool = Instantiate(_poolPrefab, _poolGenerationPoint);
          currentPool.transform.position = new Vector3(transform.position.x, transform.position.y - _deepth);
          _deepth += _spaceBetween;
          currentPool.GetComponent<Spawner>().SetPrefabs(new List<GameObject>{_fishes[i], _fishes[i + 1]});
          _pools.Add(currentPool);
      }
  }
}
