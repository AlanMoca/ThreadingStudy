using System;
using UnityEngine;
using System.Collections;
using System.Threading;

//Se evita usar el multithreading porque es complicado de manejar
public class Scale3 : MonoBehaviour
{
    AutoResetEvent resetEvent;
    Thread thread;
    bool goingDown;
    Transform t;
    Vector3 latestScale;
    bool stop = false;

    private void Start()
    {
        t = transform;
        latestScale = t.localScale;
        thread = new Thread( Run );
        thread.Start();
        resetEvent = new AutoResetEvent( false );
    }

    private void Run()
    {
        DateTime time = DateTime.Now;
        while ( !stop )
        {
            resetEvent.WaitOne();

            //Haremos esto porque no podemos usar el deltaTime de unity. Es un equivalente?
            var now = DateTime.Now;
            var deltaTime = now - time;
            time = DateTime.Now;

            latestScale += latestScale * ( goingDown ? -1f : 1f ) * (float)(deltaTime.TotalSeconds);    //Cambiamos el valor de los "1's" porque ahora es cerca del tiempo real para poder ver mejor el cambio.

            if ( ( goingDown && latestScale.magnitude < 1 ) || ( !goingDown && latestScale.magnitude > 5 ) )
            {
                goingDown = !goingDown;
            }
        }
    }

    private void Update()
    {
        resetEvent.Set();
        t.localScale = latestScale;
    }

    private void FixedUpdate()
    {
        
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
