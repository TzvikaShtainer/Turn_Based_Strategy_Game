using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestructibleCrate : MonoBehaviour
{
    public static event EventHandler OnAnyDestroyed;

    [SerializeField] private Transform destroyedCratePrefab;

    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public void Damage()
    {
        Transform crateDestroyed = Instantiate(destroyedCratePrefab, transform.position, transform.rotation);
        
        ApplyExplosionToChilds(crateDestroyed, 150f, transform.position, 10f);
        
        Destroy(gameObject);
        
        OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }
    
    private void ApplyExplosionToChilds(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExplosionToChilds(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}
