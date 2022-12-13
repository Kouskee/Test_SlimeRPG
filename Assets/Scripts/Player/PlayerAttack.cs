using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _middlePoint;
        [SerializeField] private float _delayFiring = 5;
        [SerializeField] private int _damage = 10;
        [SerializeField] private Projectile _projectile;
        
        private List<Movement> _enemies;
        private Movement _nearestEnemy;

        public void Init(List<Movement> enemies)
        {
            _enemies = enemies;
            StartCoroutine(StartShootingRoutine());
        }

        private void Start()
        {
            EventManager.OnEnemyDied.AddListener(EnemyDied);
            EventManager.OnLevelCleared.AddListener(StopAllCoroutines);
        }

        private void EnemyDied(Transform enemyTransform)
        {
            _enemies.Remove(enemyTransform.GetComponent<Movement>());
        }

        public void ChangeRateAttack(float boost)
        {
            if(_delayFiring - boost > 0)
                _delayFiring -= boost;
        }
        
        public void ChangePowerAttack(int power)
        {
            _damage += power;
        }

        #region Shooting

        private IEnumerator StartShootingRoutine()
        {
            while (true)
            {
                if (_enemies.Count <= 0)
                {
                    EventManager.OnLevelCleared.Invoke(); // a very bad decision, the player should not be responsible for the level in any way
                    break;
                }
                
                var minDistance = float.MaxValue;
                for (var i = 0; i < _enemies.Count; i++)
                {
                    var distance = Vector3.Distance(_enemies[i].transform.position, transform.position);
                
                    if (!(distance < minDistance)) continue;
                
                    minDistance = distance;
                    _nearestEnemy = _enemies[i];
                }
                Shoot();

                yield return new WaitForSeconds(_delayFiring);
            }
        }

        private void Shoot()
        {
            var newBullet = Instantiate(_projectile, transform.position + Vector3.up * 1, Quaternion.identity);
            newBullet.Init(_nearestEnemy.transform, _middlePoint, transform, _damage);
        }
        
        #endregion
    }
}
