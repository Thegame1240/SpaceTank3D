using UnityEngine;

namespace Cannon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffect;
    
        private void OnTriggerEnter(Collider other)
        {
            Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
