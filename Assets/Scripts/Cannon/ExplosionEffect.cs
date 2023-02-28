using Car;
using UnityEngine;
using Utilities;

namespace Cannon
{
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private int damage = 10 ;
    
        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            
        }
    }
}
