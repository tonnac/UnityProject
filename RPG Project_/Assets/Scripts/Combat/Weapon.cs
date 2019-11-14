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
        [SerializeField] 
        GameObject equippedPrefab = null;
        public float WeaponRange {get => weaponRange;}
        public float WeaponDamage {get => weaponDamage;}
        public void Spawn(Transform handTransform, Animator animator)
        {
            if(null != equippedPrefab)
            {
                Instantiate(equippedPrefab, handTransform);
            }
            if(null != animatorOverride)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}