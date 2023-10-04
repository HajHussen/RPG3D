using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] float respawnTime = 5;
        [SerializeField] Weapon weaponToPickup = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weaponToPickup);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true);
        }

        private void ShowPickUp(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

    }
}
