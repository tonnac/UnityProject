namespace RPG.Resources
{
    using RPG.Saving;
    using UnityEngine;
    
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        public float ExperiencePoint { get => experiencePoints;}
        public void GainExperience(float experience)
        {
            experiencePoints += experience;
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