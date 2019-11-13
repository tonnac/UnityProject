namespace RPG.Saving
{
    using RPG.Core;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AI;
    using SaveDict = System.Collections.Generic.Dictionary<string, object>;

    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour 
    {
        [SerializeField] string uniqueIdentifier = string.Empty;
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            SaveDict state = new SaveDict();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            SaveDict stateDict = (SaveDict)(state);

            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if(stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }
        private void Update() 
        {
            if(Application.IsPlaying(gameObject)) return;
            if(string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            if(string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

        }
    }
}