using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour {
  [SerializeField]
  private float _damage;

  private void OnTriggerEnter(Collider other)
  {
    Character target = other.gameObject.GetComponent<Character>();
    if (target != null)
    {
      target.TakeDamage(_damage);
    }
    Destroy(gameObject);
  }
}
