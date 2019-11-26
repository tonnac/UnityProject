namespace RPG.UI.DamageText
{
    using UnityEngine;
    using UnityEngine.UI;

    public class DamageText : MonoBehaviour 
    {
        [SerializeField] Text damageText = null;
        public void SetValue(float amount)
        {
            damageText.text = string.Format("{0:0}", amount);
        }

        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}