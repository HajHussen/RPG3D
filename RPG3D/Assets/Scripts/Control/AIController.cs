using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;
        Health health;
        Fighter fighter;
        Mover mover;

        Vector3 guardPosition;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition=transform.position;
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
                mover.StartMoveAction(guardPosition);
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
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(gameObject.transform.position, chaseDistance);
        }
    }
}
