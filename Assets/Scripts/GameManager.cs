using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GroundPiece[] allGroundPieces;
    private bool isAnimating = false;

    public float cameraSpeed = 7.5f;
    public float animationTime = 1.5f;
    private float initialCameraZoom = 52.9f;
    private float finalCameraZoom = 10.0f;

    void Start()
    {
        SetupNewLevel();
        BackgroundMusic.instance.PlayMusic();
        Camera.main.orthographicSize = initialCameraZoom;
    }

    void Update()
    {
        if (isAnimating)
        {
            Camera.main.orthographicSize -= cameraSpeed * Time.deltaTime;
            if (Camera.main.orthographicSize <= finalCameraZoom)
            {
                isAnimating = false;
                Camera.main.orthographicSize = finalCameraZoom;
            }
        }
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
            StartCoroutine(AnimateCamera());
        }
    }

    IEnumerator AnimateCamera()
    {
        // Animation logic here
        isAnimating = true;

        // Call the next level callback after the animation is done
        yield return new WaitForSeconds(animationTime);
        isAnimating = false;
        NextLevel();
    }

    public void NextLevel()
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
        Camera.main.orthographicSize = initialCameraZoom;
    }
}
