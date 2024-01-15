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
        transform.Translate(Vector3.down * speed * Time.smoothDeltaTime);

        if (transform.position.y <= end)

        {
            Vector3 pos = new Vector3(0, start, 5);
            transform.position = pos;
        }
    }
}
