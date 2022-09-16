using UnityEngine;
using Cinemachine;

public class CinemachinePlayer : MonoBehaviour
{
    [SerializeField]
    private bool isTesting = false;

    private CinemachineVirtualCamera cmvirtualCam;

    private void Awake()
    {
        cmvirtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        if (isTesting) SetCamFollowTarget();
        GameManager.Instance.OnPlayerSpawned += SetCamFollowTarget;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= SetCamFollowTarget;
    }

    private void SetCamFollowTarget()
    {
        cmvirtualCam.Follow = GameManager.Instance.GetPlayer().transform;
    }
}
