using UnityEngine;
using System.Collections;

public class MiniBird : MonoBehaviour
{

    void Start()
    {

        StartCoroutine(DestroyAfterTime(7f));
    }

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
