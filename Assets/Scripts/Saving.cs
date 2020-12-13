using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Saving : MonoBehaviour
{
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        LoadPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.position.z);
        Debug.Log(PlayerPrefs.GetFloat("PlayerPositionX"));
    }

    public void LoadPosition()
    {
        playerPosition.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ"));
    }
}
