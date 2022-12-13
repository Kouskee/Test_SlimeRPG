using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private HealthBar _hpBar;
        [SerializeField] private float _health;
   
        public void TakeDamage(int damage)
        {
            _health -= damage;
            _hpBar.UpdateParams(_health/100);

            if (_health > 0) return;
            EventManager.OnEnemyDied.Invoke(transform);
            Destroy(gameObject);
        }
    }
}