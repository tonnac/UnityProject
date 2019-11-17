namespace RPG.Stats
{
    using System;
    using RPG.Saving;
    using UnityEngine;
    
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;
        public event Action onExperienceGained;
        public float ExperiencePoint { get => experiencePoints;}
        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }
        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }    
}