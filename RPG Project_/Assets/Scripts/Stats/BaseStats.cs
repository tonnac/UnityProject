namespace RPG.Stats
{
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour 
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
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