using UnityEngine;
using UnityEngine.SceneManagement;

//Script to load the correct game on the front end.
public class LoadSceneByIndx : MonoBehaviour {

	public void loadSceneIndex(int indx)
	{
		//Loads the scene by the given index.
		SceneManager.LoadScene (indx);
	}
}
