namespace RPG.Combat
{
    using System;
    using UnityEngine;
    
    public class Projectile : MonoBehaviour 
    {
        [SerializeField] float speed = 1f;
        [SerializeField] Transform target = null;

        private void Update() 
        {
            if(null == target) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            return null == targetCapsule ? target.position : target.position + Vector3.up * targetCapsule.height * 0.5f;
        }
    }
}