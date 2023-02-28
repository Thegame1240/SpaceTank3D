using Car;
using UnityEngine;

namespace Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody bulletPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Transform playerTransform;

        private double fireRate = 1f;
        private float fireCounter = 0;
    
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            playerTransform = GameObject.FindObjectOfType<PlayerCar>().transform;
            fireCounter += Time.deltaTime;
            LaunchProjectile();
        }

        void LaunchProjectile()
        {
            if (fireCounter >= fireRate)
            {
                Vector3 Vo = CalculateVelocity(playerTransform.position, shootPoint.position, 1f);
                transform.rotation = Quaternion.LookRotation(Vo);
                Rigidbody fire = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                fire.velocity = Vo;
                fireCounter = 0;
                DestroyBullet();
            }
        }

        Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ_plane = distance;
            distanceXZ_plane.y = 0f;

            float distanceXZ = distanceXZ_plane.magnitude;
            float distanceY = distance.y;

            float Vxz = distanceXZ / time;
            float Vy = distanceY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

            Vector3 result = distanceXZ_plane.normalized;
            result *= Vxz;
            result.y = Vy;

            return result;
        }

        private void DestroyBullet()
        {
            var bullets = GameObject.FindGameObjectsWithTag("Bullet");
            for (int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i], 1.5f);
            }
        }
    }
}
