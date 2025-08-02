using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public float Force = 100f;
    public float LifeTime = 1f;

    private Rigidbody CurrencyRb;

    private void Awake()
    {
        CurrencyRb = GetComponent<Rigidbody>();
        StartCoroutine(DespawnCurrencyPrefab());
    }

    void Start()
    {
        CurrencyRb.AddForce(transform.up * Force);
    }

    IEnumerator DespawnCurrencyPrefab()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
