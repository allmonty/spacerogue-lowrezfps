using UnityEngine;
using System.Collections;

public class PlayerShootControl : MonoBehaviour {

    public GameObject bullet = null;
    public Transform spawnPoint = null;
    public float bulletSpeed = 2.0f;
    public float angularSpeed = 2.0f;

    [Header("SFX")]
    public AudioSource fireSFX = null;
    public float minPitch = 0.5f;
    public float maxPitch = 1.5f;

    public void shoot()
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = spawnPoint.position;
        Rigidbody bulletRigid = newBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = spawnPoint.forward * bulletSpeed;

        Vector3 randomVec3 = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        bulletRigid.angularVelocity = randomVec3 * angularSpeed;

        fireSFX.pitch = Random.Range(minPitch, maxPitch);
        fireSFX.Play();
    }
}
