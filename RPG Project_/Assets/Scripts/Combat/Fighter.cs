using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;
        Health target;
        float timeSinceLastAttack = float.PositiveInfinity;

        private void Start() 
        {
            SpawnWeapon();
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

        // Aniumation Event
        void Hit()
        {
            if(null == target) return;
            target.TakeDamage(weapon.WeaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.WeaponRange;
        }

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

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        private void SpawnWeapon()
        {
            if(weapon == null) return;
            weapon.Spawn(handTransform, GetComponent<Animator>());
        }
    }
}