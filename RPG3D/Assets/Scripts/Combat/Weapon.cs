using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRighthanded = true;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController weaponAnimatorOverride = null;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public void SpawnWeapon(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyPreviousWeapon(rightHand, leftHand);

            if (weaponPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (weaponAnimatorOverride != null)
            {
                animator.runtimeAnimatorController = weaponAnimatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;

            }
        }

        private void DestroyPreviousWeapon(Transform rightHand, Transform leftHand)
        {
            Transform previousWeapon = rightHand.Find(weaponName);
            if (previousWeapon == null)
            {
                previousWeapon = leftHand.Find(weaponName);
            }
            if (previousWeapon == null) return;

            previousWeapon.name = "Destroying";
            Destroy(previousWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRighthanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }
    }
}