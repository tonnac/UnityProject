namespace RPG.Saving
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using SaveDict = System.Collections.Generic.Dictionary<string, object>;
    
    public class SavingSystem : MonoBehaviour 
    {
        public IEnumerator LoadLastScene(string saveFile)
        {
            SaveDict state = LoadFile(saveFile);
            if(state.ContainsKey("lastSceneBuildIndex"))
            {
                int buildIndex = (int)state["lastSceneBuildIndex"];
                if(buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            RestoreState(state);
        }
        public void Save(string saveFile)
        {
            SaveDict state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private SaveDict LoadFile(string saveFile)
        {
            print($"Loading from {GetPathFromSaveFile(saveFile)}");
            if(!File.Exists(GetPathFromSaveFile(saveFile)))
            {
                return new SaveDict();
            }
            using (FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (SaveDict)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(string saveFile, SaveDict state)
        {
            print($"Saving to {GetPathFromSaveFile(saveFile)}");
            using (FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(SaveDict state)
        {
            Array.ForEach(FindObjectsOfType<SaveableEntity>(), saveable => 
            state[saveable.GetUniqueIdentifier()] = saveable.CaptureState());
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(SaveDict state)
        {
            Array.ForEach(FindObjectsOfType<SaveableEntity>(), saveable =>
            {
                string id = saveable.GetUniqueIdentifier();
                if(state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            });
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, $"{saveFile}.sav");
        }
    }
}