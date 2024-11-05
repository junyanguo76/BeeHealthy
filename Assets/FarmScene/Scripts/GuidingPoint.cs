using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
