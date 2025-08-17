using System.Collections;
using System;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float minX = -0.5f;
    [SerializeField] private float maxX = 0.5f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 0.5f;
    private Action<NormalBullet> onRecycle;

    public void Init(Action<NormalBullet> onRecycle)
    {
        this.onRecycle = onRecycle;
    }

    public void SetUp(GameObject go)
    {
        transform.parent = go.transform;
    }

    public void Move(float force)
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        Vector3 randomDir = transform.forward + new Vector3(randomX, randomY);
        rb.AddForce(randomDir * force, ForceMode.Impulse);
    }

    public IEnumerator Delete(float existTime)
    {
        yield return new WaitForSeconds(existTime);
        Destroy(gameObject);

    }

    public IEnumerator Recycle(float existTime)
    {
        yield return new WaitForSeconds(existTime);
        onRecycle?.Invoke(this);
    }

}
