
namespace RPG.SceneManagement
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using RPG.Saving;
    using UnityEngine;

    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        private void Load()
        {
            //call to saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}