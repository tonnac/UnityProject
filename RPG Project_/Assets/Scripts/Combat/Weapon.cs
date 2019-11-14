namespace RPG.Combat
{
    using RPG.Core;
    using UnityEngine;
    using UnityEngine.Serialization;

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {        
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [FormerlySerializedAs("equippedPrefab")]
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] bool isRigghtHanded = true;
        [SerializeField] Projectile projectile = null;
        public float WeaponRange {get => weaponRange;}
        public float WeaponDamage {get => weaponDamage;}
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(null != equippedPrefab)
            {
                Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
            }
            if (null != animatorOverride)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return isRigghtHanded ? rightHand : leftHand;
        }

        public bool HasProjectile()
        {
            return null != projectile;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }
    }
}