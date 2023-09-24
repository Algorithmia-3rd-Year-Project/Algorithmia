using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
      [Header("File Storage Config")] [SerializeField]
      private string fileName;
      
      private GameData gameData;

      private List<IDataPersistence> dataPersistenceObjects;

      private FileDataHandler dataHandler;
      
      public static DataPersistenceManager instance { get; private set; }

      private void Awake()
      {
            if (instance != null)
            {
                  Debug.LogError("Found more than one data persistence manager in the scene.");
            }

            instance = this;
      }

      private void Start()
      {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
      }
 
      public void NewGame()
      {
            this.gameData = new GameData();
      }

      public void LoadGame()
      {
            //Todo - Load any save data from a file
            this.gameData = dataHandler.Load();
            
            //if no data can be loaded, initialize to a new game
            if (this.gameData == null)
            {
                  Debug.Log("No data was found. Initializing data to defaults");
                  NewGame();
            }
            
            //push the loaded data into all other scripts that needed them
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                  dataPersistenceObj.LoadData(gameData);
            }
            
            Debug.Log("Loaded coin Amount : " + gameData.coinAmount);
      }

      public void SaveGame()
      {
            //Pass the data to other scripts so they can update it
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                  dataPersistenceObj.SaveData(ref gameData);
            }
            
            Debug.Log("Saved coins : " + gameData.coinAmount);
            
            //Todo - save that data to a file using the data handler
            dataHandler.Save(gameData);
      }

      private void OnApplicationQuit()
      {
            SaveGame();
      }

      private List<IDataPersistence> FindAllDataPersistenceObjects()
      {
            IEnumerable<IDataPersistence> dataPersistenceObjects =
                  FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
      }
}
