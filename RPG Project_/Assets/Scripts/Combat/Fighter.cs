using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0f;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(null == target) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancle();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > timeBetweenAttack)
            {
                // This will trigger the hit event.
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        // Aniumation Event
        void Hit()
        {
            if(null != target)
            {
                Health healthComponent = target.GetComponent<Health>();
                healthComponent.TakeDamage(weaponDamage);
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancle() => target = null;
    
    }
}