using UnityEngine;

namespace RPG.Combat
{

    public class Health : MonoBehaviour
    {
        bool isDead = false;

        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
            if (health == 0 && !isDead)
            {
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
        }
    }
}
