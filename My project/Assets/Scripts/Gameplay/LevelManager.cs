using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;
using static UnityEngine.GraphicsBuffer;

public class LevelManager : MonoBehaviour
{
    public GameObject wormholePrefab; 
    public Transform wormholeSpawnPoint; 
    public Game gameInstance; 
    public Movement movementScript; 
    public MonoBehaviour[] spawnScripts; 
    public AudioClip LevelSound;  
    public AudioClip PortalSound;  
    private AudioSource audioSource;
    public AudioSource bgmSource; 
    public FlyingObjects objects;
    public LevelCompleteEffect texteffect;
    public Canvas fadeCanvas; 
    private CanvasGroup fadeCanvasGroup; 

    public float fadeDuration = 2f; 
    private float startTime;
    private bool Ended;

    void Start()
    {
        startTime = Time.time; 
        audioSource = Camera.main.GetComponent<AudioSource>();
        if (fadeCanvas != null)
        {
            fadeCanvasGroup = fadeCanvas.GetComponentInChildren<CanvasGroup>();
        }
    }

    void Update()
    {
        if (Time.time - startTime >= 120f)
        {
            if (!Ended)
            {
                EndLevel();
            }
        }
    }

    public void EndLevel()
    {
        Ended = true;

        // Disable player input
        if (gameInstance != null)
        {
            gameInstance.Input.Disable();
        }

        // Disable movement script
        if (movementScript)
        {
            movementScript.enabled = false;
        }

        // Disable all enemy spawners
        foreach (var spawner in spawnScripts)
        {
            if (spawner)
                spawner.enabled = false;
        }


        // Destroy all spawned objects
        if (objects != null)
        {
            objects.DestroyAllSpawnedObjects();
        }

        StartCoroutine(MovePlayerToCenter());
    }
    IEnumerator FadeOutAndPlay()
    {
        if (bgmSource != null)
        {
            float startVolume = bgmSource.volume;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                bgmSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
                yield return null;
            }

            bgmSource.volume = 0;
            bgmSource.Stop();
        }
        audioSource.PlayOneShot(LevelSound);
        texteffect.EndText();
    }

    private IEnumerator TransitionAndFade()
    {
        Transform playerTransform = movementScript.transform;
        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = new Vector3(55f, playerTransform.position.y, playerTransform.position.z);
        float elapsedTime = 0f;


        float shakeDuration = 0.5f; 
        float shakeIntensity = 0.2f; 
        Vector3 originalPosition = playerTransform.position; 

        while (elapsedTime < shakeDuration)
        {
            movementScript.thruster.Play();
            float shakeX = Random.Range(-shakeIntensity, shakeIntensity);
            float shakeY = Random.Range(-shakeIntensity, shakeIntensity);
            playerTransform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        elapsedTime = 0f; 
        while (elapsedTime < 2f)
        {
            movementScript.thruster.Play();
            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / 2f));
            if (fadeCanvasGroup != null)
            {
                fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / 2f);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition; 

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
        }

        SceneManager.LoadScene("Level 2");
    }




    IEnumerator MovePlayerToCenter()
    {
        Transform playerTransform = movementScript.transform;
        Vector3 startPosition = playerTransform.localPosition;
        float transitionDuration = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            movementScript.thruster.Play();
            playerTransform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        

        playerTransform.localPosition = Vector3.zero;

        StartCoroutine(FadeOutAndPlay());
        yield return new WaitForSeconds(4f);
        audioSource.PlayOneShot(PortalSound);
        if (wormholePrefab && wormholeSpawnPoint)
        {
            Instantiate(wormholePrefab, wormholeSpawnPoint.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(TransitionAndFade());
    }
}
