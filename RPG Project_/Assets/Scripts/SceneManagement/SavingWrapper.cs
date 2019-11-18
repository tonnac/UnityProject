
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
        [SerializeField] float fadeInTime = 0.2f;

        private void Awake() 
        {
            StartCoroutine(LoadLastScene());
        }
        IEnumerator LoadLastScene() 
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }
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
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
        public void Load()
        {
            //call to saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        private void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}