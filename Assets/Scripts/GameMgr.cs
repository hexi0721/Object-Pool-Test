using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{

    [SerializeField] private Canon canon;
    [SerializeField] private Button shootBtn;
    [SerializeField] private Button changeBtn;

    private void Start()
    {
        canon.Init();

        shootBtn.onClick.AddListener(() => { canon.Shoot(); });
        changeBtn.onClick.AddListener(() => { canon.Change(); });
    }

    private void Update()
    {
        canon.Shoot();
    }
}
