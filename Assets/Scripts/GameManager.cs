using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;

    [SerializeField] List<GameObject> prefabsObjects;
    int index;

    Vector3 posi;
    float randmonXPosi;
    float xRange = 4.4f;
    float yPosi = -2.0f;

    float randomTime;
    internal float minTimeRange = 1.0f;
    internal float maxTimeRange = 2.5f;

    [SerializeField] TextMeshProUGUI gameOverText;
    internal bool isGameActive;
    [SerializeField] Button restartButton;
    [SerializeField] internal TextMeshProUGUI scoreText;
    int score;

    AudioSource audioSource;
    [SerializeField] AudioClip forGoodSFX, forBadSFX;

    // Start is called before the first frame update
    internal void StartGame()
    {
        startScreen.gameObject.SetActive(false);

        score = 0;
        UpdateScore(0);
        isGameActive = true;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(WaitTime());
    }

    void SpawnObj()
    {
        if (isGameActive)
        {
            index = Random.Range(0, prefabsObjects.Count);
            Instantiate(prefabsObjects[index], CreateRandomPosi(), prefabsObjects[index].transform.rotation);
        }
    }

    Vector3 CreateRandomPosi()
    {
        randmonXPosi = Random.Range(-xRange, xRange);
        posi.Set(randmonXPosi, yPosi, 0);
        return posi;
    }
    
    IEnumerator WaitTime()
    {
        while (isGameActive)
        {
            randomTime = Random.Range(minTimeRange, maxTimeRange);
            yield return new WaitForSeconds(randomTime);
            SpawnObj();
        }
    }

    internal void UpdateScore(int additoinValue)
    {
        score += additoinValue;
        scoreText.text = "Score : " + score;
    }

    internal void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        isGameActive = false;

        List<GameObject> goodAndBad = new List<GameObject>();
        goodAndBad.AddRange(GameObject.FindGameObjectsWithTag("Good"));
        goodAndBad.AddRange(GameObject.FindGameObjectsWithTag("Bad"));
        foreach (var obj in goodAndBad)
        {
            Destroy(obj);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    internal void PlaySFX(string whithObj)
    {
        if (whithObj == "Explosion_Black") audioSource.PlayOneShot(forBadSFX, 2.0f);
        else audioSource.PlayOneShot(forGoodSFX, 2.0f);
    }
}
