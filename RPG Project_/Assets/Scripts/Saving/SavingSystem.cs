namespace RPG.Saving
{
    using System.IO;
    using System.Text;
    using UnityEngine;
    
    public class SavingSystem : MonoBehaviour 
    {
        public void Save(string saveFile)
        {
            print($"Saving to {GetPathFromSaveFile(saveFile)}");
            using(FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Create))
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Hola Mundo!");
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public void Load(string saveFile)
        {
            print($"Loading from {GetPathFromSaveFile(saveFile)}");
            using(FileStream stream = File.Open(GetPathFromSaveFile(saveFile), FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                print(Encoding.UTF8.GetString(buffer));
            }
        }

        // private byte[] SerializeVector(Vector3 vector)
        // {

        // }

        // private Vector3 DeserializeVector(byte[] buffer)
        // {

        // }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, $"{saveFile}.sav");
        }
    }
}