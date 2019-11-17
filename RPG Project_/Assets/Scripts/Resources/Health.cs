using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Range(1, 100)]
        [SerializeField] float regenerationPercentage = 70f;
        float healthPoints = -1f;
        bool isDead = false;

        private void Start() 
        {
            BaseStats baseStats = GetComponent<BaseStats>();
            if(healthPoints < 0)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
            baseStats.onLevelUp += RegenerateHealth;
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

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage * 0.01f;
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
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