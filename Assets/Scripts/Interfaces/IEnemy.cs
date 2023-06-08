using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy: ICharacter
{
    public void SpawnEnemy();

    public void ChasePlayer();
}
