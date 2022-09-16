using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    protected SoundEffectSO onPickedUpSFX;

    private IPickupable pickupable;

    private void Start()
    {
        pickupable = GetComponent<IPickupable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            pickupable.OnPickedUp(player);
            SoundEffectManager.Instance.PlaySoundEffect(onPickedUpSFX, transform.position, Quaternion.identity, true);
            //GameManager.Instance.GetPlayer().ResetAndPlaySound(GameManager.Instance.GetPlayer().experienceGainedSFX, onPickedUpSFX);
        }
    }
}
