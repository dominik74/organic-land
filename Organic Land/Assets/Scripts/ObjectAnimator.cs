using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour {

    public float animationDuration = 1f;

	public void Animate()
    {
        StopCoroutine("Shake");
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        transform.Rotate(new Vector3(35f, 0, 0));
        yield return new WaitForSeconds(animationDuration / 2);
        transform.Rotate(new Vector3(-35f, 0, 0));
        //yield return new WaitForSeconds(animationDuration / 2);
    }

}
