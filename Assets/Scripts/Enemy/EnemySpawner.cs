using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyBezierCurves;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;
#pragma warning disable 4014

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public PlayerAttack _player;
        [Header("Settings")]
        [SerializeField] private Movement _prefab;
        [SerializeField] private BezierCurve3D _path;
        [Tooltip("Count enemy between two points")]
        [SerializeField] private Vector2Int _limit;
        
        private List<Movement> _enemies;
    
        private void Start()
        {
            _enemies = new List<Movement>(_limit.y);
            
            EventManager.OnLevelCleared.AddListener(OnLevelCleared);
            OnLevelCleared();
        }

        private void OnLevelCleared()
        {
            StopAllCoroutines();
            var countEnemyToSpawn = Random.Range(_limit.x, _limit.y);
            SpawnEnemy(countEnemyToSpawn);
        }
        
        private async Task SpawnEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var enemy = Instantiate(_prefab, transform);
                enemy.Init(_path, i * 2);
                _enemies.Add(enemy);
                await Task.Delay(TimeSpan.FromSeconds(.1f));
            }
            
            _player.Init(_enemies);
        }
    }
}
