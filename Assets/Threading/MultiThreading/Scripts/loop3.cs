using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//Unity is already Multi-Threading (Actualmente unity tiene multi hilos). Con cada pequeña iteration unity mueve más y más tareas como le es posible al background thread. Por ejemplo (creo) the physics engine corre
// completamente en un hilo separado lo que es una de las razones  que se pueda tener el FixedUpdate function. Esta funcion se ejecuta cada que el "physics background thread" está listo para iniciar o reiniciar.
//Es importante porque es cuando sincronizas el main thread y el physics thread.
public class loop3 : MonoBehaviour
{
    private bool isRunning = false;

    private void Start()
    {
        Debug.Log( "Start() :: Starting" );
        //>SlowJob es sólo una VARIABLE que contiene algún codigo de función (así son todas las variables!) Cuando nosotros agregamos "()" al final del nombre de esta variable no es más que un shotcut o forma de decirle a c# que querremos que "EJECUTE EL CONTENIDO DE LA VARIABLE SLOWJOB".
        //Es por eso que en la instricción de abajo sólo pasamos el nombre como referencia a la variable que contiene el código ya que no querremos aún que se ejecute.
        Thread myThread = new Thread( SlowJob );
        myThread.Start();   //Ejecutara SlowJob en un nuevo hilo.
        Debug.Log( "Start() :: Done" );
    }

    private void Update()
    {
        if ( isRunning )
            Debug.Log( "SlowJob isRunning" );       //Esta línea podría seguirse ejecutando antes del debug "Done ElapsedTime", ya que son procesos separados y cuando se vuelva falso este podría ejecutar unos cuántos más antes.
    }

    private void SlowJob()
    {
        isRunning = true;

        Debug.Log( "Doing 1000 things, each taking 2ms" );
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        for ( int i = 0; i < 1000; i++ )
        {
            Thread.Sleep( 2 );  //SleepTimeout 2ms
        }

        //Note: Porque son varios overheads el tiempo transcurrido será poco más de 2 segundos
        Debug.Log( "Done! Elapsed Time: " + sw.ElapsedMilliseconds / 1000f );
        isRunning = false;
    }

}
