namespace RPG.Combat
{
    using RPG.Core;
    using UnityEngine;
    
    public class Projectile : MonoBehaviour 
    {
        [SerializeField] float speed = 1f;
        Health target = null;
        float damage = 0f;

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }
        private void Update() 
        {
            if(null == target) return;
            if(target.IsDead)
            {
                Destroy(gameObject);
                return;
            }
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

        private void OnTriggerEnter(Collider other) 
        {
            if(other.GetComponent<Health>() != target) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}