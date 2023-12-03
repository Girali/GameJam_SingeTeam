using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Lootable : MonoBehaviour
{
    // Parameters
    [SerializeField] private UnityEvent OnLooted;
    
    private void Start()
    {
        _hasBeenLooted = false;
        _isInsideLootArea = false;
    }

    public bool Looted
    {
        get => _hasBeenLooted;
    }

    [SerializeField] private GameObject visuals;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenLooted)
        {
            _isInsideLootArea = true;
            StartCoroutine(Loot());
            GUI_Controller.Instance.lootingLoad.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenLooted)
        {
            _isInsideLootArea = false;
            GUI_Controller.Instance.lootingLoad.SetActive(false);
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
            OnLooted.Invoke();
            GameController.Instance.AddLoot();
            GUI_Controller.Instance.lootingLoad.SetActive(false);

            _hasBeenLooted = true;
            visuals.SetActive(false);
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
