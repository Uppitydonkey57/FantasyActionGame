using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnCollision : MonoBehaviour
{
    public Action[] actions;

    public Vector3 size;

    public LayerMask layer;

    public bool doOnce;

    bool hasPerformed;

    public Color collisionColor = Color.white;

    // Update is called once per frame
    void Update()
    {
        if ((hasPerformed && !doOnce) || !hasPerformed) {
            Collider[] collisions = Physics.OverlapBox(transform.position, size / 2, transform.rotation, layer);
            if (collisions.Length > 0)
            {
                Debug.Log(Physics.OverlapBox(transform.position, size, Quaternion.identity, layer)[0]);
                foreach (Action action in actions) action.PerformAction();
                hasPerformed = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 scale = transform.localScale;
        Transform matrixTransform = transform;
        matrixTransform.localScale = Vector3.one;
        Gizmos.matrix = matrixTransform.localToWorldMatrix;
        transform.localScale = scale;

        Gizmos.color = collisionColor;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
