using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public bool IsDead {get => isDead;}
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if(isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}