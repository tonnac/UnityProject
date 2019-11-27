namespace RPG.Combat
{
    using UnityEngine;
    
    public class Weapon : MonoBehaviour 
    {
        public void OnHit()
        {
            print($"Weapon Hit {gameObject.name}");
        }
    }
}