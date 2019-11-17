namespace RPG.Stats
{
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour 
    {
        [Range(1, 99)]
        [SerializeField] int level;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, level);
        }
    }
}