namespace RPG.Saving
{
    using System.IO;
    using UnityEngine;
    
    public class SavingSystem : MonoBehaviour 
    {
        public void Save(string saveFile)
        {
            print($"Saving to {GetPathFromSaveFile(saveFile)}");
        }

        public void Load(string saveFile)
        {
            print($"Loading from {GetPathFromSaveFile(saveFile)}");
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, $"{saveFile}.sav");
        }
    }
}