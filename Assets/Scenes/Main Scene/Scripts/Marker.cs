using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    void OnBecameInvisible() {
         Destroy(gameObject);
     }
}
