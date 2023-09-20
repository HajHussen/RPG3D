using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] string sceneToLoadName;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(sceneToLoadName);
            }
        }
    }
}
