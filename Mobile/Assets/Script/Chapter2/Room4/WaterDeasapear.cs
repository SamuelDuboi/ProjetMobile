using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeasapear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        var startSize = main.startSize;
        StartCoroutine(WaterCaroutine(main, startSize));
    }

    IEnumerator WaterCaroutine(ParticleSystem.MainModule mainModule, ParticleSystem.MinMaxCurve startsize)
    {
        float timer = 0;
        while (timer < 3)
        {
            timer += 0.01f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(startsize.constantMin - timer*2, startsize.constantMax - timer*2);
            mainModule.gravityModifier = new ParticleSystem.MinMaxCurve(6 + timer*2, 6 + timer*2);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
