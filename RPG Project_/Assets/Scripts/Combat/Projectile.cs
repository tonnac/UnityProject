namespace RPG.Combat
{
    using RPG.Core;
    using UnityEngine;
    
    public class Projectile : MonoBehaviour 
    {
        [SerializeField] float speed = 1f;
        Health target = null;

        public void SetTarget(Health target)
        {
            this.target = target;
        }
        private void Update() 
        {
            if(null == target) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            return null == targetCapsule ? 
            target.transform.position : 
            target.transform.position + Vector3.up * targetCapsule.height * 0.5f;
        }
    }
}