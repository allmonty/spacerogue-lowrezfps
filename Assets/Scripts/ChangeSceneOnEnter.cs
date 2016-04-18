using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnter : MonoBehaviour {

    public string nextScene = "StartGame";

	void Update () {
	    if(Input.GetButton("Submit"))
        {
            SceneManager.LoadScene(nextScene);
        }
	}
}
