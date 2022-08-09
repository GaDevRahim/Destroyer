using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsMovement : MonoBehaviour
{
    Rigidbody objRB;
    float randomForce;
    float maxForce = 15.0f;
    float minForce = 12.0f;
    float randomTorque;
    float torqueValue = 10;

    GameManager gameManager;
    [SerializeField] int additoinValue;
    [SerializeField] ParticleSystem explosion;

    void Start()
    {
        objRB = GetComponent<Rigidbody>();
        objRB.AddForce(Vector3.up * GetRandomForce(), ForceMode.Impulse);
        objRB.AddTorque(GetRandomTorque(), GetRandomTorque(), GetRandomTorque(), ForceMode.Impulse);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    float GetRandomForce()
    {
        randomForce = Random.Range(minForce, maxForce);
        return randomForce;
    }

    float GetRandomTorque()
    {
        randomTorque = Random.Range(-torqueValue, torqueValue);
        return randomTorque;
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(additoinValue);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
            string whichObj = explosion.gameObject.name;
            gameManager.PlaySFX(whichObj);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Good")) gameManager.GameOver();
        Destroy(gameObject);
    }
}
