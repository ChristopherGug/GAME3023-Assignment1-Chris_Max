using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEncounter : MonoBehaviour
{
    public int encounterChance;
    private int randomNumber;
    public Saving saveFile;

    public PlayerController playerController;
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
        if (other.tag == "GrassTrigger" && playerController.isMoving)
        {
            Debug.Log("werghwerg");
            randomNumber = Random.Range(0, encounterChance);
            if (randomNumber == 3)
            {
                saveFile.SavePosition();
                saveFile.LoadPosition();
                TransitionScene();
            }
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
