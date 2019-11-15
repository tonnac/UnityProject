namespace RPG.Combat
{
    using System;
    using RPG.Core;
    using UnityEngine;
    using UnityEngine.Serialization;

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {        
#region Private Fields
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [FormerlySerializedAs("equippedPrefab")]
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] bool isRigghtHanded = true;
        [SerializeField] Projectile projectile = null;
        readonly string weaponName = "Weapon";
#endregion

#region Public Methods
        public float WeaponRange {get => weaponRange;}
        public float WeaponDamage {get => weaponDamage;}
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if(null != equippedPrefab)
            {
                GameObject weapon = Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
                weapon.name = weaponName;
            }
            if (null != animatorOverride)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
        public bool HasProjectile()
        {
            return null != projectile;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }
#endregion
#region Private Methods
        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName) == null ? leftHand.Find(weaponName) : rightHand.Find(weaponName);
            if(oldWeapon == null) return;
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return isRigghtHanded ? rightHand : leftHand;
        }
#endregion
    }
}