using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
