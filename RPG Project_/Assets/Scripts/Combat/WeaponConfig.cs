﻿namespace RPG.Combat
{
    using RPG.Attributes;
    using UnityEngine;
    using UnityEngine.Serialization;

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject 
    {        
#region Private Fields
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float percentageBonus = 0f;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [FormerlySerializedAs("equippedPrefab")]
        [SerializeField] Weapon equippedPrefab = null;
        [FormerlySerializedAs("isRigghtHanded")]
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        readonly string weaponName = "Weapon";
#endregion

#region Public Methods
        public float PercentageBonus {get => percentageBonus;}
        public float WeaponRange {get => weaponRange;}
        public float WeaponDamage {get => weaponDamage;}
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if(null != equippedPrefab)
            {
                Weapon weapon = Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
                weapon.gameObject.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (null != animatorOverride)
            { 
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if(null != overrideController)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }
        public bool HasProjectile()
        {
            return null != projectile;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
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
            return isRightHanded ? rightHand : leftHand;
        }
#endregion
    }
}