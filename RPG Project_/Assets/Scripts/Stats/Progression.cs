namespace RPG.Stats
{
    using System.Linq;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        public float GetHealth(CharacterClass characterClass, int level)
        {
            //TODO
            var a = from progressionClass in characterClasses 
            where progressionClass.characterClass == characterClass && progressionClass.stats.Length >= level
            select progressionClass.stats[level - 1];

            if(a.Count() <= 0)
            {
                Debug.LogError($"{characterClass}, Level: {level} is null");
            }

            return a.SingleOrDefault().levels[0];
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats = null;
            //public float[] health = null;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}