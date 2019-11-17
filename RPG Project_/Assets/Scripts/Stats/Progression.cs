namespace RPG.Stats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];

            return levels.Length >= level ? levels[level - 1] : 0;

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

            // var a = from progressionClass in characterClasses 
            // where progressionClass.characterClass == characterClass
            // select progressionClass.stats;

            // if(a.Count() < 1)
            // {
            //     Debug.LogError($"{characterClass}, stats is null");
            // }

            // var b = from progressionStat in a.Single()
            // where progressionStat.stat == stat && progressionStat.levels.Length >= level
            // select progressionStat.levels[level - 1];

            // if(b.Count() < 1)
            // {
            //     Debug.LogError($"{stat} of {characterClass}, Level: {level} is null");
            // }

            // return b.SingleOrDefault();
        }

        private void BuildLookup()
        {
            if(null != lookupTable) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                Dictionary<Stat, float[]> statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
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