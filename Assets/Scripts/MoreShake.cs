using UnityEngine;

public class MoreShake : MonoBehaviour 
{
    [SerializeField] private ScreenShake shake;
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float shakeMax;
    [SerializeField] private AnimationCurve shakeCurve;
    private float shakeAmount;

    private void Start() 
    {
        shake = GetComponent<ScreenShake>();
    }

    private void FixedUpdate() 
    {
        shake.Shake(1, Mathf.Clamp(shakeCurve.Evaluate(shakeAmount), 0, shakeMax));
        shakeAmount += increaseSpeed;
    }
}