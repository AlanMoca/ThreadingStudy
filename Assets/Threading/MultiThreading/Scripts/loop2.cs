using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//En un hilo normal todo se ejecuta de manera lineal instrucción por instrucción pero en un multithreading tú no sabes exactamente qué instrucción de qué hilo se ejecutará después por lo que no puedes estar seguro
//de llevar una secuencia ya que se ejecutan simultaneamente.

//Las corutines son multitareas coperativas (cooperative multitasking)
//Algunos consideran las corutinas algo confusas y estupidas porque al final no puedes hacer uso del 100% de tu máquina porque siempre tienes que decirle algún momento en que se pare... ? E intentan y prefieren no usarlas.
public class loop2 : MonoBehaviour
{
    private bool isRunning = false;

    private void Start()
    {
        Debug.Log( "Start() :: Starting" );
        StartCoroutine( SlowJob() );
        Debug.Log( "Start() :: Done" );
    }

    private void Update()
    {
        if ( isRunning )
            Debug.Log( "SlowJob isRunning" );
    }

    private IEnumerator SlowJob()
    {
        isRunning = true;

        Debug.Log( "Doing 1000 things, each taking 2ms" );
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        for ( int i = 0; i < 1000; i++ )
        {
            Thread.Sleep( 2 );  //SleepTimeout 2ms
            yield return null;                              //<-- La corutina se detendra en esta línea por un frame! Y dejará descansar lo demás hasta el siguiente frame que volverá a iniciar debajo de esta línea.
        }

        //Note: Porque son varios overheads el tiempo transcurrido será poco más de 2 segundos
        Debug.Log( "Done! Elapsed Time: " + sw.ElapsedMilliseconds / 1000f );
        isRunning = false;

        yield return null;

    }

}
