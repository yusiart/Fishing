using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Spawner : ObjectPool
{
   [SerializeField] private List<GameObject> _prefabs;
   //[SerializeField] private Transform _hook;
   [SerializeField] private Transform [] _spawnPoints;
   
    Random _random = new Random();
   private void Start()
   {
      foreach (var prefab in _prefabs)
      {
         Initialize(prefab);
      }
   }

   private void Update()
   {
      //int spawnPointNumber = _random.NextInt(0, _spawnPoints.Length);
         
      if (TryGetObject(out GameObject fish))
      {
         SetEnemy(fish,_spawnPoints[0].position);
      }
   }

   private void SetEnemy(GameObject prefab, Vector2 spawnPoint)
   {
      prefab.transform.position = spawnPoint;
      prefab.SetActive(true);
   }
}
