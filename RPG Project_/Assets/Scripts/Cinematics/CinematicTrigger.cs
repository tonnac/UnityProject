﻿using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        private bool alreadyTriggered = false;
        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player" && !alreadyTriggered)
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
        public object CaptureState()
        {
            return alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            alreadyTriggered = (bool)state;
        }
    }
}