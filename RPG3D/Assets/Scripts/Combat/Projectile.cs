using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 2;
        [SerializeField] bool homingProjectile = false;
        [SerializeField] GameObject hitEffect = null;

        Health target = null;

        float damage = 0;
        bool targetingEnemy = true;


        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            if (targetingEnemy & !homingProjectile)
            {
                transform.LookAt(GetAimLocation());
                targetingEnemy = false;
            }
            if (homingProjectile && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            target.TakeDamage(damage);
            if (hitEffect != null) Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            Destroy(gameObject);
        }
    }
}
