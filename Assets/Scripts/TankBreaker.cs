using System.Collections;
using UnityEngine;

public class TankBreaker : MonoBehaviour
{
    [SerializeField] private float resetDistance = 1f; // ����������, ��� ������� ������ ��������� "������������"

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

        // ������� ��������� "FixedJoint" �� ������� �������
        foreach (Transform child in children)
        {
            Destroy(child.GetComponent<FixedJoint>());
        }

        // ������� ��������� ������� ��� ������� �������
        foreach (Transform child in children)
        {
            // ���� ��� �� ��� ������, �� �������� ��� � ��������������� Game Object
            if (child != transform)
            {
                // ������� ����� ������ Game Object ��� �������� ��������� �������
                GameObject newRoot = new GameObject("Separate Child");

                // ���������� �������� ������ � ����� ������
                child.SetParent(newRoot.transform, false);
               
            }
        }
    }
}