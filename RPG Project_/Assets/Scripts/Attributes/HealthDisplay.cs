namespace RPG.Attributes
{
    using UnityEngine;
    using UnityEngine.UI;
    
    public class HealthDisplay : MonoBehaviour 
    {
        Health health;
        private void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update() 
        {
            GetComponent<Text>().text = $"{health.GetHealthPoints()} / {health.GetMaxHealthPoints()}";
        }
    }
}