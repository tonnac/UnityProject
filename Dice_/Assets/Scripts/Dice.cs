namespace Marble.Dice
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Dice : MonoBehaviour
    {
        public List<Vector3> directions = new List<Vector3>();
        public Transform[] transforms;
        public ForceMode forceMode;
        public float qwe;
        Rigidbody rb = null;
        bool hasLanded;
        bool thrown;
        Vector3 initPosition;
        public int diceValue;

        public DiceSide[] diceSides;
        public float force;

        public float gravityScale = 1.5f;
 
    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
 
        public static float globalGravity = -9.81f;
 
        private void OnValidate() {
            print("Onv");
            if(rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            rb.maxAngularVelocity = qwe;
        }
        private void Awake() 
        {
            rb = GetComponent<Rigidbody>();
            initPosition = transform.position;
            rb.useGravity = false;
            rb.maxAngularVelocity = qwe;

            foreach (var item in transforms)
            {
                if(item.gameObject.activeSelf)
                {
                    directions.Add((item.position - transform.position).normalized);
                }
            }
        }
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                RollDice();
            }

            if(rb.IsSleeping() && !hasLanded && thrown)
            {
                hasLanded = true;
                rb.useGravity = false;
                SideValueCheck();
            }
            else if(rb.IsSleeping() && hasLanded && diceValue == 0)
            {
                RollAgain();
            }
        }
        private void RollDice()
        {
            if(!thrown && !hasLanded)
            {
                ThrowDice();
            }
            else if(thrown && hasLanded)
            {
                Reset();
            }
        }

        private void ThrowDice()
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(transform.up * rb.maxAngularVelocity );
            rb.AddTorque(transform.right * rb.maxAngularVelocity );
            rb.AddTorque(transform.forward * rb.maxAngularVelocity );
            rb.AddForce(directions[Random.Range(0, directions.Count - 1)] * force);
        }

        private void Reset() 
        {
            transform.position = initPosition;
            thrown = false;
            hasLanded = false;
            rb.useGravity = false;
            diceValue = 0;
        }
        private void RollAgain()
        {
            Reset();
            ThrowDice();
        }

        void SideValueCheck()
        {
            diceValue = 0;
            Array.ForEach(diceSides, (elem) =>
            {
                if(elem.onGround)
                {
                    diceValue = elem.sideValue;
                    print($"dice {7 - diceValue}");
                }
            });
        }
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            foreach (var item in directions)
            {
                Gizmos.DrawLine(transform.position, item * 500);
            }
        }
    }
}
