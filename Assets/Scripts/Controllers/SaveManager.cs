using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string filePath;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        filePath = Application.persistentDataPath + "data.gamesave";

        SaveGame();
        LoadGame();
    }

    public void SaveGame()
    {

        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            var save = new Save();
            save.QuantityApple = GameManager.QuantityApple;
            save.Score = GameManager.Score;

            bf.Serialize(fs, save);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        using (var fs = new FileStream(filePath, FileMode.Open))
        {
            var bf = new BinaryFormatter();
            var save = (Save)bf.Deserialize(fs);

            GameManager.QuantityApple = save.QuantityApple;
            GameManager.Score = save.Score;
        }
    }

    [System.Serializable]
    public class Save
    {
        public int QuantityApple;
        public int Score;
    }
}
