namespace RPG.Combat
{
    using RPG.Core;
    using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
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
            if(null != hitEffect)
            {
                GameObject effectObj = Instantiate(hitEffect, GetAimLocation(), target.transform.rotation);
                effectObj.GetComponent<ParticleSystem>().Play();
                Destroy(effectObj, effectObj.GetComponent<ParticleSystem>().main.duration);
            }
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}