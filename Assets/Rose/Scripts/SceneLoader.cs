using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    
    public void NextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
