using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private SceneChange nextScene;

    [SerializeField] private float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(Open());
        }
    }

    IEnumerator Open() 
    {
        if (animator != null) animator.SetTrigger("Open");

        yield return new WaitForSeconds(waitTime);

        nextScene.ChangeScene();
    }
}
