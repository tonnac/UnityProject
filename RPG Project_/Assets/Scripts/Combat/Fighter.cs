using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
#region Private Fields        
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] string defaultWeaponName = "Unarmed";
        Health target;
        float timeSinceLastAttack = float.PositiveInfinity;
        LazyValue<WeaponConfig> currentWeapon = null;
#endregion        
#region MonoBehaviour Callbacks
        private void Awake()
        {
            currentWeapon = new LazyValue<WeaponConfig>(GetInitialWeapon);
        }

        private void Start() 
        {
            currentWeapon.ForceInit();
        }

        private WeaponConfig GetInitialWeapon()
        {
            WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(defaultWeaponName);
            AttachWeapon(weapon);
            return weapon;
        }


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(null == target) return;
            if(target.IsDead) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancle();
                AttackBehaviour();
            }
        }
#endregion        
#region Public Methods
        public Health GetTarget() => target;
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }
        
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancle()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancle();
        }
        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(WeaponConfig weapon)
        {
            weapon.Spawn(rightHandTransform, leftHandTransform, GetComponent<Animator>());
        }
        #endregion
        #region ISaveable Implementation
        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            EquipWeapon(UnityEngine.Resources.Load<WeaponConfig>((string)state));
        }
#endregion
#region IModifierProvider Implementation
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.WeaponDamage;
            }
        }
        
        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.PercentageBonus;
            }
        }

#endregion        
#region Private Methods        
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttack)
            {
                // This will trigger the hit event.
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.WeaponRange;
        }        
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        private WeaponConfig GetWeapon()
        {
            return UnityEngine.Resources.Load<WeaponConfig>(defaultWeaponName);
        }
#endregion
#region Animation Event
        void Hit()
        {
            if(null == target) return;

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if(currentWeapon.value.HasProjectile())
            {
                currentWeapon.value.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }
        void Shoot()
        {
            Hit();
        }
        #endregion
    }
}