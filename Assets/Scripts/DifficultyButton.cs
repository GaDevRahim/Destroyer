using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{

    List<Button> buttonList = new List<Button>();

    Button button;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDiffecalty);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.startScreen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetDiffecalty()
    {
        switch (button.gameObject.name)
        {
            case "Easy":
                gameManager.minTimeRange = 1.5f;
                gameManager.maxTimeRange = 3.0f;
                gameManager.scoreText.gameObject.SetActive(true);
                break;
            case "Normal":
                gameManager.minTimeRange = 1.0f;
                gameManager.maxTimeRange = 2.0f;
                gameManager.scoreText.gameObject.SetActive(true);
                break;
            case "Hard":
                gameManager.minTimeRange = 0.5f;
                gameManager.maxTimeRange = 1.0f;
                gameManager.scoreText.gameObject.SetActive(true);
                break;
            default: break;
        }
        gameManager.StartGame();
    }

    void DisappearButtons()
    {

    }
}
