using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

    public GameObject bullet = null;
    public Transform spawnSpot = null;
    public float bulletSpeed = 5f;
    public float delay = 0.5f;

    Vector3 target;
    bool isShooting = false;

	public void shoot(Vector3 target)
    {
        if(!isShooting)
        {
            this.target = target;
            isShooting = true;
            Invoke("doShoot", delay);
        }
    }

    void doShoot()
    {
        GameObject newBullet = Instantiate(bullet) as GameObject;
        newBullet.transform.position = spawnSpot.position;

        Vector3 targetDir = (target - spawnSpot.position).normalized;
        newBullet.GetComponent<Rigidbody>().velocity = targetDir * bulletSpeed;

        isShooting = false;
    }
}
