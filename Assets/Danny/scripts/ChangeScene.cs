using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public void SwitchScene()
    {
        Debug.Log("Did it Work?");
        SceneManager.LoadScene(SceneName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SwitchScene();
        }
    }
}
