using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKiller : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(DestroyYourself());
    }

    private IEnumerator DestroyYourself()
    {
        yield return new WaitForSeconds(3);
        ObjectPool.Instance.Despawn(GetComponent<TimeKiller>());
    }
}
