using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject boat;

    private LevelData currentLevel;
    private UIManager uiManager;

    private GameObject respawnPoint;
    private HighAngleCamera cam;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            // Menu Scene
        }
        else
        {
            InitializeLevel();
        }
    }

    public void DroppedObject()
    {
        currentLevel.objectDropped++;
        uiManager.UpdateDrops(currentLevel.objectDropped);
    }

    public void StartReset()
    {
        StartCoroutine("Delay");
    }

    public void CompleteLevel()
    {
        
    }

    private void InitializeLevel()
    {
        currentLevel = FindObjectOfType<LevelData>();
        uiManager = FindObjectOfType<UIManager>();
        cam = FindObjectOfType<HighAngleCamera>();

        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");

        uiManager.UpdateDrops();
        uiManager.UpdatePar(currentLevel.par);

        CreateBoat();
    }

    private void Reset()
    {
        currentLevel.objectDropped = 0;
        uiManager.UpdateDrops();

        CreateBoat();
    }

    private void CreateBoat()
    {
        GameObject newBoat = Instantiate(boat, respawnPoint.transform.position, Quaternion.identity);
        cam.boat = newBoat.transform;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        Reset();
    }
}
