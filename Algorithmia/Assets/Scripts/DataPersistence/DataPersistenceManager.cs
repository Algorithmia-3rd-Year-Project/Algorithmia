using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

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
                  Debug.LogError("Found more than one data persistence manager in the scene. Destroying the newest one");
                  Destroy(this.gameObject);
                  return;
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
      }

      private void OnEnable()
      {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
      }

      private void OnDisable()
      {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
      }

      public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
      {
            Debug.Log("OnSceneLoaded Called");
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
      }

      public void OnSceneUnloaded(Scene scene)
      {
            Debug.Log("OnSceneUnLoaded Called");
            SaveGame();
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
                  Debug.Log("No data was found. A game needs to be started before data can be loaded");
                  //NewGame();
                  return;
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
            if (this.gameData == null)
            {
                  Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved");
                  return;
            }
            
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

      public bool HasGameData()
      {
            return gameData != null;
      }
}
