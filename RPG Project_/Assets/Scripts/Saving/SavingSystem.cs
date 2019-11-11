namespace RPG.Saving
{
    using UnityEngine;
    
    public class SavingSystem : MonoBehaviour 
    {
        public void Save(string saveFile)
        {
            print($"Saving to {saveFile}");
        }

        public void Load(string saveFile)
        {
            print($"Loading from {saveFile}");
        }
    }
}