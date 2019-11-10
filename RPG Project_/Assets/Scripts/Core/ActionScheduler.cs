using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour 
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if(currentAction == action) return;
            if(null != currentAction)
            {
               currentAction.Cancle();
            }
            currentAction = action;
        }

        public void CancleCurrentAction()
        {
            StartAction(null);
        }
    }
}