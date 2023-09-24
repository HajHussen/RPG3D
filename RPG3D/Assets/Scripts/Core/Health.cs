using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{

    public class Health : MonoBehaviour, ISaveable
    {
        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }

        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
            if (health == 0 && !isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            isDead = true;
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0 && !isDead)
            {
                Die();
            }
        }
    }
}
