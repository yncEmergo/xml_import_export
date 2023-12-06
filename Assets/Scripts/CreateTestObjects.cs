using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTestObjects : MonoBehaviour
{
    public void CreateCubeWithRandomForce()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = new Vector3(0, 0, 0);

        Rigidbody rb = cube.AddComponent<Rigidbody>();

        rb.useGravity = true;

        Vector3 randomForce = new Vector3(Random.Range(-5f, 5f), Random.Range(5f, 15f), Random.Range(-5f, 5f));
        rb.AddForce(randomForce, ForceMode.Impulse);

        StartCoroutine(DestroyObject(cube));
    }

    IEnumerator DestroyObject(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }
}
