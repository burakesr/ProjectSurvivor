[System.Serializable]
public class AbilityStats
{
    public int damage;
    public float timeToAttack;

    public AbilityStats(int damage, float timeToAttack)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
    }
}
