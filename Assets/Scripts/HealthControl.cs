using UnityEngine;
using System.Collections;

public class HealthControl : MonoBehaviour {

    public AudioSource damageSound;
    public int lifePoints = 2;

	public void takeDamage(int dmgPoints)
    {
        lifePoints -= dmgPoints;

        damageSound.Play();

        if(lifePoints <= 0)
        {
            die();
        }
    }

    virtual protected void die()
    {
        Destroy(gameObject);
    }
}
