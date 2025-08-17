using UnityEngine;

public class NormalBullet : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float minX = -0.5f;
    [SerializeField] private float maxX = 0.5f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 0.5f;

    public void SetUp(GameObject go)
    {
        transform.parent = go.transform;
    }

    public void Move(float force)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 randomDir = transform.forward + new Vector3(randomX, randomY);
        rb.AddForce(randomDir * force, ForceMode.Impulse);
    }

}
