using UnityEngine;
using RPG.Core;
using RPG.Combat;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;
        Health health;
        Fighter fighter;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (!GetIsInChaseRange())
            {
                fighter.Cancel();
            }
            if (GetIsInChaseRange())
            {
               fighter.Attack(player);
            }
        }
        private bool GetIsInChaseRange()
        {
            return Vector3.Distance(player.transform.position, gameObject.transform.position) < chaseDistance;
        }
    }
}
