using UnityEngine;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
        void Update()
        {
            if (GetIsInChaseRange())
            {
                print($"{gameObject.name} is chasing Player!");
            }
        }
        private bool GetIsInChaseRange()
        {
            return Vector3.Distance(player.transform.position, gameObject.transform.position) < chaseDistance;
        }
    }
}
