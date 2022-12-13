using System.Collections;
using Enemy;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _delayToDestroy;
        [SerializeField] private float _forceCurve;
        
        private int _damage;
        private float _count;
        private Transform[] _point;

        public void Init(Transform enemy, Transform middlePoint, Transform player, int damage)
        {
            _point = new Transform[3];
            _point[0] = player;
            _point[1] = middlePoint;
            _point[2] = enemy;
            _damage = damage;
        }
        
        private void Start()
        {
            StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(_delayToDestroy);
            Destroy(gameObject);
        }
        
        private void Update()
        {
            if(_point == null || _point[2] == null) return;
            
            _point[1].position = _point[0].position + (_point[2].position - _point[0].position) / 2 + Vector3.up * _forceCurve;
                
            if (_count < 1.0f) {
                _count += 1.0f *Time.deltaTime;

                Vector3 m1 = Vector3.Lerp( _point[0].position, _point[1].position, _count );
                Vector3 m2 = Vector3.Lerp( _point[1].position, _point[2].position, _count );
                transform.position = Vector3.Lerp(m1, m2, _count);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Health health)) return;
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}