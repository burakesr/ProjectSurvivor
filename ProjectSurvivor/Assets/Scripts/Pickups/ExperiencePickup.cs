using UnityEngine;

public class ExperiencePickup : Pickup, IPickupable
{
    [SerializeField] private int experienceAmount = 100;

    public void OnPickedUp(Player player)
    {
        player.GetLevelManager.AddExperience(experienceAmount);
        gameObject.SetActive(false);
    }
}