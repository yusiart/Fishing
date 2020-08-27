﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
   [SerializeField] private ObjectPool _objectPool;


   private void OffAllFishes()
   {
      _objectPool.OffFishes();
   }
}
