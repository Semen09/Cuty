using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads given name scene
/// </summary>
public class LoadScene : MonoBehaviour {
    [SerializeField]
    string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LoadSceneGM(sceneToLoad);
        }
    }
    public void LoadSceneGM(string name)
    {
        if (GameManager.gm)
        {
            GameManager.gm.LoadScene(name);
        }
    }
}
