namespace RPG.Stats
{
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour 
    {
        [Range(1, 99)]
        [SerializeField] int level;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, level);
        }
    }
}