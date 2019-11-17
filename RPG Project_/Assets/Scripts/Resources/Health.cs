using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        private void Start() 
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public bool IsDead {get => isDead;}

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints <= 0f)
            {
                Die();
                AwardExperience(instigator);
            }
        }
        public float GetPercentage()
        {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health) * 100f;
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(null == experience) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        private void Die()
        {
            if(isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancleCurrentAction();
        }
        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if(healthPoints == 0)
            {
                Die();
            }
        }
    }
}