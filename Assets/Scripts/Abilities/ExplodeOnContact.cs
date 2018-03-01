using UnityEngine;

public class ExplodeOnContact : MonoBehaviour {
  [SerializeField]
  private float _damage;

  private Character _caster;

  public void SetCaster(Character caster)
  {
    _caster = caster;
  }

  private void OnTriggerEnter(Collider other)
  {
    Character target = other.gameObject.GetComponent<Character>();
    if (target != null)
    {
      if (target == _caster)
      {
        return;
      }
      target.TakeDamage(_damage);
    }
    Destroy(gameObject);
  }
}
