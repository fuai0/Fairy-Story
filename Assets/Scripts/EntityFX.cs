using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    public static EntityFX instance;
    private SpriteRenderer sr;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void BloodFX()
    {
        sr.color = Color.red;

    }

}
