using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    public Transform PlayerrTransform;

    private Vector3 cmOffset;

    public float smoothness = 0.5f, borderLeft = -10, borderRight = 2.5f;

    public bool isCloud = false;

    [SerializeField]
    private GameObject camHolder;

    // Start is called before the first frame update
    void Start()
    {
        cmOffset = transform.position - PlayerrTransform.position;
    }

    private void FixedUpdate()
    {
        if ((PlayerrTransform.position.x > borderLeft && PlayerrTransform.position.x < borderRight) || isCloud)
        {
            Vector3 newPos = PlayerrTransform.position + cmOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origin = camHolder.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camHolder.transform.localPosition = new Vector3(x, y, origin.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        camHolder.transform.localPosition = origin;
    }
}
