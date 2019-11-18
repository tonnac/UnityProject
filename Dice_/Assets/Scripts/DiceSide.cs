namespace Marble.Dice
{
    using System;
    using UnityEngine;
    
    public class DiceSide : MonoBehaviour 
    {
        public bool onGround {get; set;}

        public int sideValue;
        private void OnTriggerStay(Collider col)
        {
            if(col.tag == "Ground")
            {
                onGround = true;
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.tag == "Ground")
            {
                onGround = false;
            }
        }
    }
}