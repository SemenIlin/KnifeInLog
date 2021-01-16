using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private GameValues _gameValues;

    private string filePath;

    private void Awake()
    {
        Vibration.Init();
        if (Instance == null)
        {
            Instance = this;
        }
        filePath = Application.persistentDataPath + "data.gamesave";

        _gameValues = GameValues.Instance;

        LoadGame();
        SaveGame();
    }

    public void SaveGame()
    {
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            var save = new Save();
            save.QuantityApple = _gameValues.QuantityApple;
            save.MaxScore = _gameValues.MaxScore;
            save.MaxLevel = _gameValues.MaxLevel;

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

            _gameValues.SetQuantityOfApples(save.QuantityApple);
            _gameValues.SetMaxScore(save.MaxScore);
            _gameValues.SetMaxLevel(save.MaxLevel);
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
