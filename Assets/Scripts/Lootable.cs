using System;
using System.Collections;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    private void Start()
    {
        _hasBeenLooted = false;
        _isInsideLootArea = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenLooted)
        {
            _isInsideLootArea = true;
            StartCoroutine(Loot());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenLooted)
        {
            _isInsideLootArea = false;
        }
    }

    private IEnumerator Loot()
    {
        yield return new WaitForSeconds(2f);

        if (_isInsideLootArea)
        {
            GameObject vfx = null;
            
            if (lootVFX)
            {
                vfx = Instantiate(lootVFX, transform.position + (Vector3.up * 5f), lootVFX.transform.rotation);
            }
            
            GameController.Instance.AddLoot();

            _hasBeenLooted = true;

            yield return new WaitForSeconds(5f);

            Destroy(gameObject);
            if (vfx)
            {
                Destroy(vfx);
            }
        }
    }

    public GameObject lootVFX;
    private bool _hasBeenLooted;
    private bool _isInsideLootArea;
}
