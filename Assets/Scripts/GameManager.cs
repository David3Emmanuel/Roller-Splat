using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GroundPiece[] allGroundPieces;
    void Start()
    {
        SetupNewLevel();
        BackgroundMusic.instance.PlayMusic();
    }

    void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool isFinished = true;
        foreach (GroundPiece groundPiece in allGroundPieces)
        {
            if (!groundPiece.isColored)
            {
                isFinished = false;
                break;
            }
        }

        if (isFinished)
        {
            Debug.Log("Level Complete");
            NextLevel();
        }
    }

    void NextLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentBuildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
