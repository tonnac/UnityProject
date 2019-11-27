namespace RPG.Attributes
{
    using UnityEngine;
    
    public class HealthBar : MonoBehaviour 
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        private void Update() 
        {
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }   
    }
}