namespace RPG.UI.DamageText
{
    using UnityEngine;
    
    public class Destroyer : MonoBehaviour 
    {
        [SerializeField] GameObject TargetToDestroy = null;

        void DestroyTarget()
        {
            Destroy(TargetToDestroy);
        }
    }
}