using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public void Move();

    public void Attack();

    public void Death();

    public void GetDamage(float damage);
}