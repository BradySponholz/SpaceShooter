using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScape : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float end;
    [SerializeField]
    private float start;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.smoothDeltaTime);

        if (transform.position.y <= end)

        {
            Vector2 pos = new Vector2(0f, start);
            transform.position = pos;
        }
    }
}
