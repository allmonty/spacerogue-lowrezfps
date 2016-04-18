using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObjectiveControl : MonoBehaviour {

    public int numberOfMapsInGame = 3;
    public int numberOfMapsCaught = 0;

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            numberOfMapsCaught += 1;
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
