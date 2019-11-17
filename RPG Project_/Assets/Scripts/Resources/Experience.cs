namespace RPG.Resources
{
    using UnityEngine;
    
    public class Experience : MonoBehaviour 
    {
        [SerializeField] float experiencePoints = 0f;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }
    }    
}