using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string filePath;

    private void Awake()
    {
        Vibration.Init();
        if (Instance == null)
        {
            Instance = this;
        }
        filePath = Application.persistentDataPath + "data.gamesave";

        LoadGame();
        SaveGame();
    }

    public void SaveGame()
    {
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            var save = new Save();
            save.QuantityApple = GameManager.QuantityApple;
            save.MaxScore = GameManager.MaxScore;
            save.MaxLevel = GameManager.MaxLevel;

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
            GameManager.MaxScore = save.MaxScore;
            GameManager.MaxLevel = save.MaxLevel;
        }
    }

    [System.Serializable]
    public class Save
    {
        public int QuantityApple;
        public int MaxScore;
        public int MaxLevel;
    }
}
