using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
   public GameObject enemyPrefab;
   public int count;
   public float rate;
   public Wave(GameObject enemyPrefab,int count,float rate){
      this.enemyPrefab=enemyPrefab;
      this.count=count;
      this.rate=rate;
   }
}
