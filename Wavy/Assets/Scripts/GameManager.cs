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

    [SerializeField] private float winDelayTime = 1f;
    [SerializeField] private float resetDelayTime = 2f;
    private LevelData currentLevel;
    private UIManager uiManager;

    private GameObject respawnPoint;
    private HighAngleCamera cam;
    private DropObject dropObject;

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
        dropObject.boatActive = false;
        StartCoroutine("ResetDelay");
    }

    public void PlayerReset()
    {
        Reset();
    }

    public void StartCompleteLevel()
    {
        StartCoroutine("WinDelay");
    }

    private void InitializeLevel()
    {
        currentLevel = FindObjectOfType<LevelData>();
        uiManager = FindObjectOfType<UIManager>();
        cam = FindObjectOfType<HighAngleCamera>();
        dropObject = FindObjectOfType<DropObject>();

        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");

        uiManager.UpdateDrops();
        uiManager.UpdatePar(currentLevel.par);

        CreateBoat();
    }

    private void Reset()
    {
        if (cam.boat != null)
        {
            Destroy(cam.boat.gameObject);
        }

        currentLevel.objectDropped = 0;
        uiManager.UpdateDrops();

        CreateBoat();
        dropObject.boatActive = true;
    }

    private void CompleteLevel()
    {
        Debug.Log("YIPEEEEEE");
    }

    private void CreateBoat()
    {
        GameObject newBoat = Instantiate(boat, respawnPoint.transform.position, Quaternion.identity);
        cam.boat = newBoat.transform;
    }

    private IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(resetDelayTime);
        Reset();
    }

    private IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(winDelayTime);
        CompleteLevel();
    }
}
