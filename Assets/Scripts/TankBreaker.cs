using System.Collections;
using UnityEngine;

public class TankBreaker : MonoBehaviour
{
    [SerializeField] private float resetDistance = 1f; // Расстояние, при котором объект считается "перемещенным"

    private Vector3 initialPosition;
    private Transform[] children;
    private bool isBroken = false;

    void Start()
    {
        initialPosition = transform.position;
        children = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (isBroken)
            return;

        float distanceFromInitial = Vector3.Distance(transform.position, initialPosition);

        if (distanceFromInitial > resetDistance)
        {
            BreakApart();
        }
    }

    private void BreakApart()
    {
        isBroken = true;

        // Удалить компонент "FixedJoint" из каждого ребенка
        foreach (Transform child in children)
        {
            Destroy(child.GetComponent<FixedJoint>());
        }

        // Создать отдельные объекты для каждого ребенка
        foreach (Transform child in children)
        {
            // Если это не сам корень, то выделяем его в самостоятельный Game Object
            if (child != transform)
            {
                // Создаем новый пустой Game Object для хранения дочернего объекта
                GameObject newRoot = new GameObject("Separate Child");

                // Перемещаем дочерний объект в новый корень
                child.SetParent(newRoot.transform, false);
               
            }
        }
    }
}