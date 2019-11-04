using System;
using UnityEngine;
using System.Collections;
using System.Threading;

//Ningún elemento de unity puede ser ejecutado fuera del hilo principal (main thread), tiene que hacerse de manera implicita.
//Recordar detener el hilo!
public class Scale1 : MonoBehaviour
{
    Thread thread;
    bool goingDown;
    Transform t;
    Vector3 latestScale;
    bool stop = false;

    private void Start()
    {
        //Inicializamos thread
        t = transform;
        latestScale = t.localScale;     //indirectamente
        thread = new Thread( Run );
        thread.Start();
    }

    private void Run()
    {
        while ( !stop )
        {
            latestScale += latestScale * ( goingDown ? -0.01f : 0.01f );
            if ( ( goingDown && latestScale.magnitude < 1 ) || ( !goingDown && latestScale.magnitude > 5 ) )
            {
                goingDown = !goingDown;
            }
        }
    }

    private void Update()
    {
        t.localScale = latestScale;
    }

    private void OnApplicationQuit()
    {
        stop = true;
        thread.Abort();
    }

    private void OnDestroy()
    {
        stop = true;
        thread.Abort();
    }

}
