namespace RPG.Saving
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using UnityEngine;
    
    public class SavingSystem : MonoBehaviour 
    {
        public void Save(string saveFile)
        {
            print($"Saving to {GetPathFromSaveFile(saveFile)}");
            using(FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile)
        {
            print($"Loading from {GetPathFromSaveFile(saveFile)}");
            using(FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            Array.ForEach(FindObjectsOfType<SaveableEntity>(), saveable =>
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            });
            return state;
        }

        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            Array.ForEach(FindObjectsOfType<SaveableEntity>(), saveable =>
            {
                saveable.RestoreState(stateDict[saveable.GetUniqueIdentifier()]);
            });
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, $"{saveFile}.sav");
        }
    }
}