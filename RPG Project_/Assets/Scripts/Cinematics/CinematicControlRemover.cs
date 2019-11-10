namespace RPG.Cinematics
{
    using RPG.Control;
    using RPG.Core;
    using UnityEngine;
    using UnityEngine.Playables;
    
    public class CinematicControlRemover : MonoBehaviour 
    {
        GameObject player;
        private void Start() 
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector director)
        {
            player.GetComponent<ActionScheduler>().CancleCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector director)
        {
            print("EnableControl");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}