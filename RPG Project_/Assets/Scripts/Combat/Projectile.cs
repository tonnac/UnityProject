namespace RPG.Combat
{
    using RPG.Resources;
    using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;
        Health target = null;
        float damage = 0f;

        private void Start() 
        {
            transform.LookAt(GetAimLocation());
        }
        private void Update()
        {
            if (null == target) return;
            if(isHoming && !target.IsDead)
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
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
            if (other.GetComponent<Health>() != target) return;
            if(target.IsDead) return;
            target.TakeDamage(damage);

            speed = 0f;

            if(null != hitEffect)
            {
                GameObject effectObj = Instantiate(hitEffect, GetAimLocation(), target.transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}