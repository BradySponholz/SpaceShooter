using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Spawner
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "Spawner/EnemyType", order = 0)]
    public class EnemyType : ScriptableObject
    {
        public GameObject enemyPrefab;
        public GameObject weaponPrefab;
        public float speed;
    }
}