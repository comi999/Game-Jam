using UnityEngine;

public class DIEPARTICVLES : MonoBehaviour
{
    private void OnEnable() =>  Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration);
}
