using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private void Awake() => gameObject.transform.localRotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
}
