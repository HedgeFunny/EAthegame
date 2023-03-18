using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    //Variables

    //General
    public Button startButton;
    public GameObject nextScene;

    public void NextScene()
    {
        SceneManager.LoadScene("IntroScene2");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
