using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stat/Character")]
public class CharaterStats : ScriptableObject
{
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public float AttackAmount;
    public int DashAmount;

    private void OnValidate()
    {
        Health = Mathf.Max(Health, 0);
        Damage = Mathf.Max(Damage, 0);
        AttackSpeed = Mathf.Max(AttackSpeed, 0);
        AttackAmount = Mathf.Max(AttackAmount, 0);
        DashAmount = Mathf.Max(DashAmount, 0);
    }
}
