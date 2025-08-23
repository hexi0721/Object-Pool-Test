using System;
using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] private GameObject notUseParent;
    [SerializeField] private GameObject useParent;
    [SerializeField] private NormalBullet normalBulletPrefab;
    [SerializeField] private float existTime;
    [SerializeField] private float force;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0.5f);
    [SerializeField] private int bulletCount;
    [SerializeField] private int preWarmCount;
    [SerializeField] private bool isUseObjectPool;
    private MyObjectPool myObjectPool = new MyObjectPool();
    private ObjectPool objectPool = new ObjectPool();
    private Action currentAction;
    private WaitForSeconds waitForExistTime;

    public void Init()
    {
        myObjectPool.Init(normalBulletPrefab, useParent, preWarmCount);
        objectPool.Init(normalBulletPrefab, preWarmCount);

        currentAction = isUseObjectPool ? UseObjectPool : NotUseObjectPool;

        waitForExistTime = new WaitForSeconds(existTime);

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
        bullet.SetUp(notUseParent , existTime);
        bullet.Move(force);
        Destroy(bullet.gameObject, existTime);
    }

    private void UseObjectPool()
    {
        NormalBullet bullet = myObjectPool.Creat(transform.position + offset, Quaternion.identity);
        bullet.SetUp(useParent , existTime);
        bullet.Move(force);
        //StartCoroutine(DelayRecycle(bullet));
    }

    private void UseObjectPool1()
    {
        NormalBullet bullet = objectPool.Get();
        bullet.transform.position = transform.position + offset;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetUp(useParent , existTime);
        bullet.Move(force);
    }

    private IEnumerator DelayRecycle(NormalBullet bullet)
    {
        yield return waitForExistTime;
        myObjectPool.Recycle(bullet);
    }

}
