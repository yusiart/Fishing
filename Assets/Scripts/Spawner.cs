using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : ObjectPool
{
   [SerializeField] private List<GameObject> _prefabs;
   [SerializeField] private GameObject _spawnPointsContainer;
   [SerializeField] private List<Transform> _spawnPoints;
   
   private int _currentPoint;

   private void Start()
   {
      GetSpawnPoints();
      
      foreach (var prefab in _prefabs)
      {
         Initialize(prefab);
      }
   }

   private void Update()
   {
      if (TryGetObject(out GameObject fish))
      {
         _currentPoint = Random.Range(0, _spawnPoints.Count);
         SetEnemy(fish,_spawnPoints[_currentPoint].position);
      }
   }

   private void SetEnemy(GameObject prefab, Vector2 spawnPoint)
   {
      prefab.transform.position = spawnPoint;
      prefab.SetActive(true);
      prefab.GetComponent<FishMover>().SetTransform(transform);
   }

   private void GetSpawnPoints()
   {
      foreach (Transform child in _spawnPointsContainer.transform)
      {
         _spawnPoints.Add(child);
      }
   }

   public void SetPrefabs(List<GameObject> prefabs)
   {
      foreach (var fish in prefabs)
      {
         _prefabs.Add(fish); 
      }
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.TryGetComponent<Hook>(out Hook hook))
      {
         this.gameObject.SetActive(true);
      }
   }
}
