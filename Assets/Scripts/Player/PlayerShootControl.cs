using UnityEngine;
using System.Collections;

public class PlayerShootControl : MonoBehaviour {

    public GameObject bullet = null;
    public Transform spawnPoint = null;
    public float bulletSpeed = 2.0f;
    public float angularSpeed = 2.0f;

    public void shoot()
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = spawnPoint.position;
        Rigidbody bulletRigid = newBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = spawnPoint.forward * bulletSpeed;

        Vector3 randomVec3 = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        bulletRigid.angularVelocity = randomVec3 * angularSpeed;
    }
}
