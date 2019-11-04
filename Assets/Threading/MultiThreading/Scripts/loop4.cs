using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//Al menos que sea un experto mis programas SÓLO DEBEN DE HACER UN TRABAJO Y TERMINAR (El hilo debe morir).
//En situaciones donde el hilo principal de unity está siempre atento del usuario y necesito hacer uso de una corutina para correr otro proceso, sólo en cosas que sean muy 
//sencillas de sólo ejecutar un método y son separados del principal puedo tal vez usar mejor un HILO. (Siempre tener la referencia que no sea una variable local por cualquier cosa) .

public class loop4 : MonoBehaviour
{
    Thread myThread;

    private void Start()
    {
        Debug.Log( "Start() :: Starting" );
        myThread = new Thread( SlowJob );
        myThread.Start(); 
        Debug.Log( "Start() :: Done" );
    }

    private void Update()
    {
        if ( myThread.IsAlive )                     //Con esto evitamos que se siga ejecutando porque en cuanto el hilo acabe(funcion asignada), no ejecutará más. 
            Debug.Log( "SlowJob isRunning" );
    }

    private void SlowJob()
    {
        Debug.Log( "Doing 1000 things, each taking 2ms" );
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        for ( int i = 0; i < 1000; i++ )
        {
            Thread.Sleep( 2 );  //SleepTimeout 2ms
        }

        //Note: Porque son varios overheads el tiempo transcurrido será poco más de 2 segundos
        Debug.Log( "Done! Elapsed Time: " + sw.ElapsedMilliseconds / 1000f );
    }

}
