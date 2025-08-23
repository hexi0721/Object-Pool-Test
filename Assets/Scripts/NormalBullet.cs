using System;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float minX = -0.5f;
    [SerializeField] private float maxX = 0.5f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 0.5f;

    private float existTime;
    private Action<NormalBullet> onRecycle;

    public void Init(Action<NormalBullet> onRecycle)
    {
        this.onRecycle = onRecycle;
    }

    // 註解掉 Canon.cs 的 Startcorotine 才能解除這邊的註解
    // private void Update()
    // {
    //     existTime -= Time.deltaTime;
    //     if (existTime <= 0)
    //     {
    //         onRecycle?.Invoke(this);
    //     }
    // }

    public void SetUp(GameObject go, float existTime)
    {
        transform.parent = go.transform;
        this.existTime = existTime;
    }

    public void Move(float force)
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        Vector3 randomDir = transform.forward + new Vector3(randomX, randomY);
        rb.AddForce(randomDir * force, ForceMode.Impulse);
    }

}
