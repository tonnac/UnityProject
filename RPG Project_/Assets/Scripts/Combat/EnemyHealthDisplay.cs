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
                GetComponent<Text>().text = string.Format("{0:0}%", health.GetPercentage());
            }
            else
            {
                GetComponent<Text>().text = "N/A";
            }
        }
    }
}