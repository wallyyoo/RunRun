using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 파괴할 수 있는 대상에 넣을 로직
public interface IDamageable 
{
  void TakeDamage(int damage);

}
