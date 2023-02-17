using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Turret : MonoBehaviour
    {
        [Header("Attributes")]
        public float range = 15f;
        public float fireRate = 1f;
        private float fireCountdown = 0f;

        [Header("Unity Setup Fields")]
        public Transform partToRotate;
        public float turretSpeed = 10f;
        private Transform target = null;

        public GameObject bulletPrefab;
        public Transform firePoint;

        private readonly string enemyTag = "Enemy";


        // Use this for initialization
        void Start()
        {
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
                return;

            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        private void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

        private void Shoot()
        {
            Debug.Log("Shoot");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}