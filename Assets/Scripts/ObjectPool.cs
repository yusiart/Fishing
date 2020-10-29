using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   [SerializeField] private GameObject _container;
   [SerializeField] private int _capacity;
   [SerializeField] private List<GameObject> pool = new List<GameObject>();
   
   protected void Initialize(GameObject prefab)
   {
      for (int i = 0; i < _capacity; i++)
      {
         GameObject spawned = Instantiate(prefab, _container.transform);
         spawned.SetActive(false);
      
         pool.Add(spawned);
      }
   }

   public bool TryGetObject(out GameObject result)
   {
      result = pool.FirstOrDefault(fish => fish.activeSelf == false);
      return result != null;
   }
}
