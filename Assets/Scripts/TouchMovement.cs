using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMovement : MonoBehaviour
{
    private float _speed = 40f;
    private float _offset;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                _offset = touch.position.y * .5f;
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y + _offset, 0));
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, _speed * Time.deltaTime);
            }
        }
    }
}
