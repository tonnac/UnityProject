using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
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
        Weapon currentWeapon = null;
#endregion        
#region MonoBehaviour Callbacks
        private void Awake()
        {
            if(null == currentWeapon)
            {
                Weapon weapon = GetWeapon();
                EquipWeapon(weapon);
            }
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
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, GetComponent<Animator>());
        }
#endregion
#region ISaveable Implementation
        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            EquipWeapon(UnityEngine.Resources.Load<Weapon>((string)state));
        }
#endregion
#region IModifierProvider Implementation
        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.WeaponDamage;
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
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.WeaponRange;
        }        
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        private Weapon GetWeapon()
        {
            return UnityEngine.Resources.Load<Weapon>(defaultWeaponName);
        }
#endregion
#region Animation Event
        void Hit()
        {
            if(null == target) return;

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if(currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
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