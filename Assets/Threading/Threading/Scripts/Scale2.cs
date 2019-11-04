using System;
using UnityEngine;
using System.Collections;
using System.Threading;

//Ningún elemento de unity puede ser ejecutado fuera del hilo principal (main thread), tiene que hacerse de manera implicita.
//Recordar detener el hilo!
public class Scale2 : MonoBehaviour
{
    AutoResetEvent resetEvent;
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
        resetEvent = new AutoResetEvent( false );       //Es un evento que funciona como evento que se autoejecuta según las intrucciones que se le pasen por lo que entendí! Aquí es falso porque estaría apgagado o sin ejecutarse?
    }

    private void Run()
    {
        while ( !stop )
        {
            resetEvent.WaitOne();   //Le decimos supongo que espere un segundo para volver a ejecutar el método o hilo que tenía... Es una pequeña pausa.
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

    private void FixedUpdate()
    {
        resetEvent.Set();   //Le decimos que sette su valor al original?
    }

    //Para asegurarnos de detener la ejecución del hilo ya que son métodos que sí o sí unity ejecutará.
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
