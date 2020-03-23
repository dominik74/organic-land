using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour {

    public float animationDuration = 0.45f;
    private Vector3 defaultEulerAngles;

    private void Start()
    {
        defaultEulerAngles = transform.eulerAngles;
    }

    public void Animate()
    {
        StopCoroutine("Shake");
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        transform.eulerAngles = new Vector3(defaultEulerAngles.x, defaultEulerAngles.y, defaultEulerAngles.z - 10f);
        yield return new WaitForSeconds(animationDuration);
        transform.eulerAngles = defaultEulerAngles;
    }

}
