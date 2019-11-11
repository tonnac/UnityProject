namespace RPG.Saving
{
    using System.IO;
    using System.Text;
    using UnityEngine;
    
    public class SavingSystem : MonoBehaviour 
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print($"Saving to {path}");
            using(FileStream stream = File.Open(path, FileMode.Create))
            {
                stream.WriteByte(0xc2);
                stream.WriteByte(0xa1);
                byte[] bytes = Encoding.UTF8.GetBytes("Hola Mundo!");
                stream.Write(bytes, 0, bytes.Length);
            }
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