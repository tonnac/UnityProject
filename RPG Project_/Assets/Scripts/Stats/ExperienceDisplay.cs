namespace RPG.Stats
{
    using RPG.Attributes;
    using UnityEngine;
    using UnityEngine.UI;

    public class ExperienceDisplay : MonoBehaviour 
    {
        Experience experience;
        private void Awake() 
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update() 
        {
            GetComponent<Text>().text = experience.ExperiencePoint.ToString();
        }
    }
}