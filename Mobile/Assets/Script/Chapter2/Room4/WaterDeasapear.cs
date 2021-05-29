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
        var startLife = main.startLifetime;
        StartCoroutine(WaterCaroutine(main, startSize, startLife));
    }

    IEnumerator WaterCaroutine(ParticleSystem.MainModule mainModule, ParticleSystem.MinMaxCurve startsize, ParticleSystem.MinMaxCurve startLife)
    {
        float timer = 0;
        while (timer < 1.5f)
        {
            timer += 0.01f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(startsize.constantMin - timer*2, startsize.constantMax - timer*2);
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(startLife.constantMin - timer*0.01f, startLife.constantMax - timer*0.01f);
            mainModule.gravityModifier = new ParticleSystem.MinMaxCurve(6 + timer*2, 6 + timer*2);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
