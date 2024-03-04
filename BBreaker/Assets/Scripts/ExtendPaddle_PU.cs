using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendPaddle_PU : MonoBehaviour
{
    public GameObject normalPaddle;
    public GameObject longPaddle;
    private Vector2 locationStore;
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.GetComponent<Ball>())
        {
            // Extend Paddle
            locationStore = normalPaddle.transform.position;
            longPaddle.transform.position = locationStore;

            longPaddle.SetActive(true);
            normalPaddle.SetActive(false);

            Destroy(this.gameObject);
        }
    }
}
