using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;

        float timeSinceLastSawPlayer = Mathf.Infinity;

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

            guardPosition = transform.position;
        }
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            EnemyAI();
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void EnemyAI()
        {
            if (GetIsInChaseRange())
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }

            else
            {
                mover.StartMoveAction(guardPosition);
            }
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
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
