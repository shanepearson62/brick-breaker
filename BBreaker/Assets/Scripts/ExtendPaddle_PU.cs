using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendPaddle_PU : MonoBehaviour
{
    public GameObject normalPaddle;
    public GameObject longPaddle;
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.GetComponent<Ball>())
        {
            // Extend Paddle
            longPaddle.SetActive(true);
            normalPaddle.SetActive(false);

            Destroy(this.gameObject);
        }
    }
}
