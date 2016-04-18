using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealthControl : HealthControl {

    protected override void die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
