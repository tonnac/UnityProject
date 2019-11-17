namespace RPG.Stats
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        public event Action onLevelUp;
        int currentLevel;


        private void Start() 
        {
            currentLevel = GetLevel();
            Experience experience = GetComponent<Experience>();
            if(null == experience) return;
            experience.onExperienceGained += UpdateLevel;
        }

        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                onLevelUp();
                LevelUpEffect();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }
        public float GetAdditiveModifier(Stat stat)
        {
            float total = 0f;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifiers in provider.GetAdditiveModifier(stat))
                {
                    total += modifiers;
                }
            }
            return total;
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel()) + GetAdditiveModifier(stat);
        }


        public int GetLevel()
        {
            if(currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
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
    }
}