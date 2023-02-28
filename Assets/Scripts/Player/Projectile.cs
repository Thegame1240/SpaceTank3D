using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody bulletPrefab;
        [SerializeField] private GameObject cursor;
        [SerializeField] private LayerMask layer;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private double fireRate = 1f;
    
        private Camera _camera;
        private float fireCounter;
    
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            fireCounter += Time.deltaTime;
            LaunchProjectile();
        }

        void LaunchProjectile()
        {
            Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(cameraRay, out hit, 100f, layer))
            {
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 0.01f;

                Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 1f);
            
                transform.rotation = Quaternion.LookRotation(Vo);
                if (fireCounter >= fireRate)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Rigidbody fire = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                        fire.velocity = Vo;
                        DestroyBullet();
                        fireCounter = 0;
                    }
                }
            }
            else
            {
                cursor.SetActive(false);
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
                Destroy(bullets[i], 2f);
            }
        }
    }
}
