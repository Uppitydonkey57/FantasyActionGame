using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public float damage;

    public float collisionStartDelay;

    public float shakeDuration;
    public float ShakeAmount;

    ScreenShake screenShake;

    public GameObject destroyParticle;

    //public bool destroyOffCamera;
    public bool useDestroyTime;
    public float destroyWait;
    public float destroyTime;

    Collider objectCollider;

    public bool destroyOnCollision = true;

    public bool stickToObject;

    SpriteRenderer rend;

    public AudioClip sound;
    AudioSource source;

    private Rigidbody rb;

    public bool playSoundOnHit;

    private void Start()
    {
        screenShake = FindObjectOfType<ScreenShake>();

        objectCollider = GetComponent<Collider>();

        StartCoroutine(StartDelay());

        if (useDestroyTime)
        {
            StartCoroutine(DestructWait());
        }

        rend = GetComponent<SpriteRenderer>();

        source = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destruct(collision.gameObject.GetComponent<Actor>(), true, collision.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Destruct(collision.gameObject.GetComponent<Actor>(), true, collision.gameObject);
    }

    IEnumerator DestructWait()
    {
        yield return new WaitForSeconds(destroyWait);

        transform.DOScale(new Vector2(0, 0), destroyTime);

        yield return new WaitForSeconds(destroyTime);

        Destruct(null, false, null);
    }

    IEnumerator StartDelay()
    {
        objectCollider.enabled = false;

        yield return new WaitForSeconds(collisionStartDelay);

        objectCollider.enabled = true;
    }

    void Destruct(Actor actor, bool showParticle, GameObject collsion)
    {
        if (destroyOnCollision)
            Destroy(gameObject);

        if (actor == null)
        {
            screenShake.Shake(shakeDuration, ShakeAmount);
        }

        if (sound != null && source != null && showParticle)
        {
            GameObject soundObject = new GameObject(gameObject.name + "'s Death Sound");
            AudioSource objectSource = soundObject.AddComponent<AudioSource>();
            objectSource.outputAudioMixerGroup = source.outputAudioMixerGroup;
            objectSource.volume = source.volume;
            objectSource.pitch = source.pitch;

            objectSource.PlayOneShot(sound);

            Destroy(gameObject, 4f);
        }

        if (stickToObject) 
        {
            objectCollider.enabled = false;

            if (collsion != null)
                transform.SetParent(collsion.transform);
                rb.isKinematic = true;
        }

        if (destroyParticle != null && showParticle)
        {
            GameObject particleInstance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(particleInstance, 10);
        }
    }
}
