using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{

    public class Health : MonoBehaviour
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
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
                GetComponent<NavMeshAgent>().enabled = false;
                isDead = true;
            }
        }
    }
}
