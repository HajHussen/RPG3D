using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        float timeSinceLastAttack = Mathf.Infinity;

        Health target;
        Weapon currentWeapon = null;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }
            if (target.IsDead())
            {
                if (target.GetComponent<Collider>())
                {
                    target.GetComponent<Collider>().enabled = false;
                }

                return;
            }
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }

        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.SpawnWeapon(handTransform, animator);
        }


        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttackAnim();
                timeSinceLastAttack = 0f;
            }
        }

        // animation event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currentWeapon.GetWeaponDamage());
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform.position, gameObject.transform.position) < currentWeapon.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            TriggerStopAttackAnim();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void TriggerAttackAnim()
        {
            GetComponent<Animator>().ResetTrigger("cancelAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private void TriggerStopAttackAnim()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("cancelAttack");
        }
    }
}

