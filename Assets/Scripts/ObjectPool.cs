using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool
{
    private NormalBullet prefab;
    private ObjectPool<NormalBullet> objectPool;


    public void Init(NormalBullet prefab, int defaultSize)
    {
        this.prefab = prefab;
        objectPool = new ObjectPool<NormalBullet>(Create, OnGet, OnRelease, null, false, defaultSize);
    }

    private NormalBullet Create()
    {
        NormalBullet t = Object.Instantiate(prefab);
        t.Init(OnRecycle);
        return t;
    }

    private void OnGet(NormalBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(NormalBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public NormalBullet Get()
    {
        return objectPool.Get();
    }

    private void OnRecycle(NormalBullet bullet)
    {
        objectPool.Release(bullet);
    }

}
