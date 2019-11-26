namespace RPG.UI.DamageText
{
    using UnityEngine;
    using UnityEngine.UI;

    public class DamageText : MonoBehaviour 
    {
        [SerializeField] Text damageText = null;
        public void SetDamage(float damage)
        {
            damageText.text = $"{damage}";
        }

        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}