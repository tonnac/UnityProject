using System;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Range(1, 100)]
        [SerializeField] float regenerationPercentage = 70f;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;

        [Serializable]
        public class TakeDamageEvent : UnityEvent<float> {}

        LazyValue<float> healthPoints;
        bool isDead = false;

        private void Awake() 
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void Start() 
        {
            healthPoints.ForceInit();
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        
        private void OnEnable() 
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable() 
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }
        public bool IsDead {get => isDead;}

        public void TakeDamage(GameObject instigator, float damage)
        {
            print($"{gameObject.name} took damage: {damage}");

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if(healthPoints.value <= 0f)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetPercentage()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health) * 100f;
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage * 0.01f;
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
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
            return healthPoints.value;
        }
        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if(healthPoints.value == 0)
            {
                Die();
            }
        }
    }
}