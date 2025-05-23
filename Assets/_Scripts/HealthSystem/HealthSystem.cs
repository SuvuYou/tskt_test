using System;

public class HealthSystem
{   
    public Action<float> OnHealthChanged { get; set; }

    private HealthStats _stats;
    public HealthState State;

    public HealthSystem(HealthStats HealthStats)
    {
        _stats = HealthStats;

        State = new HealthState()
        {
            HP = _stats.MaxHp,
        };
    }

    public void TakeDamage(int damage) 
    {
        State.HP -= damage;

        OnHealthChanged?.Invoke(State.HP);
    }
}