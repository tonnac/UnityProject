namespace RPG.Combat
{
    using UnityEngine;
    
    public class WeaponPickup : MonoBehaviour 
    {
        [SerializeField] Weapon weapon = null;
        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}