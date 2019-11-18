namespace RPG.Stats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameDevTV.Utils;
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers;
        public event Action onLevelUp;
        LazyValue<int> currentLevel;

        Experience experience;

#region MonoBehaviour Callbacks

        private void Awake() 
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(GetInitialLevel);
        }

        private int GetInitialLevel()
        {
            return CalculateLevel();
        }
        private void Start() 
        {
            currentLevel.ForceInit();
        }

        private void OnEnable() 
        {
            if(null == experience) return;
            experience.onExperienceGained += UpdateLevel;
        }

        private void OnDisable() 
        {
            if(null == experience) return;
            experience.onExperienceGained -= UpdateLevel;
        }
#endregion


#region Private Methods
        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                onLevelUp();
                LevelUpEffect();
            }
        }
        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }
        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
#endregion
#region Public Methods
        public float GetAdditiveModifiers(Stat stat)
        {
            if(!shouldUseModifiers) return 0;
            float total = 0f;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifiers in provider.GetAdditiveModifiers(stat))
                {
                    total += modifiers;
                }
            }
            return total;
        }
        public float GetPercentageModifiers(Stat stat)
        {
            if(!shouldUseModifiers) return 0;
            float total = 0f;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifiers in provider.GetPercentageModifiers(stat))
                {
                    total += modifiers;
                }
            }
            return total;
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1 + GetPercentageModifiers(stat) * 0.01f);
        }

        public int GetLevel()
        {
            if(currentLevel.value < 1)
            {
                currentLevel.value = CalculateLevel();
            }
            return currentLevel.value;
        }
        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if(null == experience) return startingLevel;
            float currentXP = experience.ExperiencePoint;
            int PenultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level < PenultimateLevel; ++level)
            {
                float XPToLevelup = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(XPToLevelup > currentXP)
                {
                    return level;
                }
            }
            return PenultimateLevel + 1;
        }
#endregion
    }
}