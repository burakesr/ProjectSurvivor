using UnityEngine;

public class CinemachineCamSwitcher : MonoBehaviour
{
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SwitchState(string cameraStateName){
        _animator.Play(cameraStateName);
    }
}