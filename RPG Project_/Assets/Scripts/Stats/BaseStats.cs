namespace RPG.Stats
{
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour 
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
    }
}