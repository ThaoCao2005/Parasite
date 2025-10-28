using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnsonTestScript : MonoBehaviour
{
    [SerializeField] RepeatableSound sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<RepeatableSound>();
    }

    // Update is called once per frame
    void Update()
    {
        sound.Play();
    }
}
