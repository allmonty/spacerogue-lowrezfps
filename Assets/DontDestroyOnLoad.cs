using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	void Start()
    {
        DontDestroyOnLoad(gameObject);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("BGMusic");
        if(objs.Length > 1)
        {
            for(int i = 1; i < objs.Length; i++)
            {
                Destroy(objs[i].gameObject);
            }
        }
    }
}
