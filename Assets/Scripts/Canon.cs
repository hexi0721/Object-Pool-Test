using System;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] private GameObject notUseParent;
    [SerializeField] private GameObject useParent;
    [SerializeField] private NormalBullet normalBulletPrefab;
    [SerializeField] private float existTime = 5f;
    [SerializeField] private float force = 1000f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0.5f);
    [SerializeField] private int bulletCount;
    [SerializeField] private int preWarmCount;
    [SerializeField] private bool isUseObjectPool;
    private MyObjectPool myObjectPool = new MyObjectPool();
    private Action currentAction;

    public void Init()
    {
        //myObjectPool.Init(normalBulletPrefab, useParent , preWarmCount);
        currentAction = NotUseObjectPool;
    }

    public void Change()
    {
        isUseObjectPool = !isUseObjectPool;
        currentAction = isUseObjectPool ? UseObjectPool : NotUseObjectPool;
    }

    public void Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            currentAction?.Invoke();
        }
    }

    private void NotUseObjectPool()
    {
        NormalBullet bullet = Instantiate(normalBulletPrefab, transform.position + offset, Quaternion.identity);
        bullet.SetUp(notUseParent);
        bullet.Move(force);
        StartCoroutine(bullet.Delete(existTime));
    }

    private void UseObjectPool()
    {
        NormalBullet bullet = myObjectPool.Creat(transform.position + offset, Quaternion.identity);
        bullet.SetUp(useParent);
        bullet.Move(force);
        StartCoroutine(bullet.Recycle(existTime));
    }

}
