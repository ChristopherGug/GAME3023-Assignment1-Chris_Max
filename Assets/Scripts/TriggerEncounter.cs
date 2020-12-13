using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEncounter : MonoBehaviour
{
    public int encounterChance;
    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            randomNumber = Random.Range(0, encounterChance);
            if (randomNumber == 3)
            {
                TransitionScene();
            }
            Debug.Log(randomNumber);
        }
    }

    private void TransitionScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    //IEnumerator Fade()
    //{
    //}
}
