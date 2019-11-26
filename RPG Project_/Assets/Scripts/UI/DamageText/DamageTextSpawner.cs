namespace RPG.UI.DamageText
{
    using UnityEngine;
    
    public class DamageTextSpawner : MonoBehaviour 
    {
        [SerializeField] DamageText damageTextPrefab = null;
        private void Start() 
        {
            Spawn(10);
        }

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}