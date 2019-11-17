namespace RPG.Stats
{
    using System.Linq;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            // foreach (ProgressionCharacterClass progressionClass in characterClasses)
            // {
            //     if(progressionClass.characterClass != characterClass) continue;

            //     foreach (ProgressionStat progressionStat in progressionClass.stats)
            //     {
            //         if(progressionStat.stat != stat) continue;
            //         if(progressionStat.levels.Length < level) continue;

            //         return progressionStat.levels[level - 1];
            //     }
            // }

            var a = from progressionClass in characterClasses 
            where progressionClass.characterClass == characterClass
            select progressionClass.stats;

            if(a.Count() < 1)
            {
                Debug.LogError($"{characterClass}, stats is null");
            }

            var b = from progressionStat in a.Single()
            where progressionStat.stat == stat && progressionStat.levels.Length >= level
            select progressionStat.levels[level - 1];

            if(b.Count() < 1)
            {
                Debug.LogError($"{stat} of {characterClass}, Level: {level} is null");
            }

            return b.SingleOrDefault();
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