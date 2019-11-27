namespace RPG.Combat
{
    using UnityEngine;
    using UnityEngine.Events;

    public class Weapon : MonoBehaviour 
    {
        [SerializeField] UnityEvent onHit;
        public void OnHit()
        {
            print($"Weapon Hit {gameObject.name}");
            onHit.Invoke();
        }
    }
}