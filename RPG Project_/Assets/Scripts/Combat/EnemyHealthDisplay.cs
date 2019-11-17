namespace RPG.Combat
{
    using RPG.Resources;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class EnemyHealthDisplay : MonoBehaviour 
    {
        Fighter fighter;
        private void Awake() 
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }
        private void Update() 
        {
            Health health = fighter.GetTarget();
            if(null != health)
            {
                GetComponent<Text>().text = $"{health.GetHealthPoints()} / {health.GetMaxHealthPoints()}";
            }
            else
            {
                GetComponent<Text>().text = "N/A";
            }
        }
    }
}