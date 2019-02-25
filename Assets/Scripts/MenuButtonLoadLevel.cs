using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // include so we can load new scenes

/// <summary>
/// Loads given name loads
/// </summary>
public class MenuButtonLoadLevel : MonoBehaviour {

	public void loadLevel(string levelToLoad)
	{
		SceneManager.LoadScene(levelToLoad);
	}
}
