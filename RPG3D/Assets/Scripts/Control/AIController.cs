using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float chaseSpeed = 4.6f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float patrolSpeed = 2f;
        [SerializeField] float waypointDwellingTime = 5f;
        [SerializeField] float waypointTolerance = 1f;

        [SerializeField] PatrolPath PatrolPath;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArriveWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

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
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArriveWaypoint += Time.deltaTime;
        }

        private void EnemyAI()
        {
            if (GetIsInChaseRange())
            {
                timeSinceLastSawPlayer = 0;
                gameObject.GetComponent<NavMeshAgent>().speed = chaseSpeed;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
                gameObject.GetComponent<NavMeshAgent>().speed = patrolSpeed;

            }

            else
            {
                Vector3 nextPosition = guardPosition;

                if (PatrolPath != null)
                {
                    if (AtWaypoint())
                    {
                        timeSinceArriveWaypoint = 0;
                        CycleWaypoint();
                    }
                    nextPosition = GetCurrentWaypoint();
                }
                if (timeSinceArriveWaypoint >= waypointDwellingTime)
                {
                    mover.StartMoveAction(nextPosition);
                }
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = PatrolPath.GetNextWaypoint(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return PatrolPath.GetWaypoint(currentWaypointIndex);
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
