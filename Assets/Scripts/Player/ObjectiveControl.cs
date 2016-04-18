using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObjectiveControl : MonoBehaviour {

    public AudioSource sound;
    public int numberOfMapsInGame = 3;
    public int numberOfMapsCaught = 0;

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            numberOfMapsCaught += 1;
            sound.Play();
            Destroy(col.gameObject);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("RescueZone"))
        {
            if(numberOfMapsCaught >= numberOfMapsInGame)
            {
                SceneManager.LoadScene("EndGame");
            }
        }
    }
}
