using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingMover : MonoBehaviour {
  private GameObject _target;
  private float _speed = 0;

  public void SetTarget(GameObject target)
  {
    _target = target;
  }

  public void SetSpeed(float speed)
  {
    _speed = speed;
  }

  private void Update()
  {
    if (_target != null)
    {
      float step = _speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
    }
  }
}
