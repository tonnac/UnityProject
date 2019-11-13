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
            SaveFile(saveFile, CaptureState());
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            print($"Loading from {GetPathFromSaveFile(saveFile)}");
            using (FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(string saveFile, Dictionary<string, object> state)
        {
            print($"Saving to {GetPathFromSaveFile(saveFile)}");
            using (FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            Array.ForEach(FindObjectsOfType<SaveableEntity>(), saveable =>
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            });
            return state;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            Array.ForEach(FindObjectsOfType<SaveableEntity>(), saveable =>
            {
                saveable.RestoreState(state[saveable.GetUniqueIdentifier()]);
            });
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, $"{saveFile}.sav");
        }
    }
}