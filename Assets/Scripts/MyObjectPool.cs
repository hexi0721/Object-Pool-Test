using System.Collections.Generic;
using UnityEngine;

public class MyObjectPool
{
    private NormalBullet prefab;
    private Queue<NormalBullet> queue = new Queue<NormalBullet>();
    List<NormalBullet> start = new List<NormalBullet>();

    public void Init(NormalBullet prefab, GameObject useParent, int preWarmCount)
    {
        this.prefab = prefab;

        for (int i = 0; i < preWarmCount; i++)
        {

            NormalBullet t = Creat(Vector3.zero, Quaternion.identity);
            t.transform.parent = useParent.transform;

            start.Add(t);

        }

        for (int i = 0; i < start.Count; i++)
        {
            Recycle(start[i]);
        }
    }

    public NormalBullet Creat(Vector3 pos, Quaternion quaternion)
    {

        if (queue.Count <= 0)
        {
            NormalBullet t = Object.Instantiate(prefab);
            t.Init(Recycle);
            queue.Enqueue(t);
        }

        NormalBullet bullet = queue.Dequeue();
        bullet.transform.position = pos;
        bullet.transform.rotation = quaternion;
        bullet.gameObject.SetActive(true);

        return bullet;
    }
    
    public void Recycle(NormalBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        queue.Enqueue(bullet);
    }
}
