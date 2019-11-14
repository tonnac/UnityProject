namespace RPG.Combat
{
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
        public float WeaponRange {get => weaponRange;}
        public float WeaponDamage {get => weaponDamage;}
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(null != equippedPrefab)
            {
                Instantiate(equippedPrefab, isRigghtHanded ? rightHand : leftHand);
            }
            if(null != animatorOverride)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}