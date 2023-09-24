using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        [SerializeField] bool isTriggered = false;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && isTriggered == false)
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
        public object CaptureState()
        {
            return isTriggered;
        }

        public void RestoreState(object state)
        {
            isTriggered = (bool)state;
        }
    }
}
