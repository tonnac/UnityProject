namespace RPG.Core
{
    using UnityEngine;
    
    public class DestroyAfterEffect : MonoBehaviour 
    {
        [SerializeField] GameObject targetToDestroy = null;
        private void Update() 
        {
            if(!GetComponent<ParticleSystem>().IsAlive())
            {
                if(null != targetToDestroy)
                {
                    Destroy(targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}