using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//Al menos que sea un experto mis programas SÓLO DEBEN DE HACER UN TRABAJO Y TERMINAR (El hilo debe morir).
//En situaciones donde el hilo principal de unity está siempre atento del usuario y necesito hacer uso de una corutina para correr otro proceso, sólo en cosas que sean muy 
//sencillas de sólo ejecutar un método y son separados del principal puedo tal vez usar mejor un HILO. (Siempre tener la referencia que no sea una variable local por cualquier cosa).

//La razón por la que no puedes modificar cosas de unity en otro hilo que no sea el principal es porque al empezar a modificar data al mismo tiempo en dos hilos distintos impredecible y horribles cosas pueden empezar a suceder.
//El 99.9% de código escrito en unity? será de un single thread.

//Se copia todo el mainData del mainThread en un temporary Spot, corro mi hilo o bien el physicsThread y cuando termina este otro hilo, take that data y la copia de nuevo en los unityObjects.

// La primera regla para el multiThreading en los videojuegos es: NO LO HAGAS NO LOS USES! Y si los llegas a usar que no deberías asegurate que se enfoquen en una sola tarea agarren solo la data que necesitan
//para hacer esa simple tarea (de preferencia data que no vaya a cambiar o que otro no vayan a requerrir en su estado original y regresa todo a como estaba antes guardando la información que obtuviste en otro lado.)

//Por último alguno encuentran más fácil de usar esto que las corutines ya que tiene mejor balance en la CPU que usar Corutinas la diferencia es que en las Corutinas se puede acceder a unity Objects
//(que si lo piensas tiene lógica porque la corutina te obliga a esperar un frame siempre forzosamente (lo que hace que haya menos rendimiento y no las puedas aprovechar al máximo pero seguramente lo hacen para
// que no haya un problema en sobreescritura de datos) ).

//MultiThreading no funciona con WEBGL!!! (WEBGL DOESN'T DO MULTITHREADING!! En algún momento lo hizo pero ya no).

public class loop5 : MonoBehaviour
{
    Thread myThread;
    string foo;
    object frontDoor = new object();

    private void Start()
    {
        Debug.Log( "Start() :: Starting" );
        foo = "Computer Science";
        myThread = new Thread( SlowJob );
        myThread.Start(); 
        Debug.Log( "Start() :: Done" );
    }

    private void Update()
    {
        if ( myThread.IsAlive )                     //Con esto evitamos que se siga ejecutando porque en cuanto el hilo acabe(funcion asignada), no ejecutará más. 
            Debug.Log( "SlowJob isRunning" );
        //this.transform.Translate( Vector3.up * Time.deltaTime ); //Funciona
        Debug.Log( foo );
    }

    void PrintStudentID()
    {
        //This send foo to the pinter
        //This should print out "Computer Science"
        //Or maybe it'll print out "English literature"
        //Either is legit (Hasta aquí es legal?)
        //What isn't legit is if in the MIDDLE of printing the other thread makes a change and suddenly we get a student ID that says: "Computer Literature".

        //Let's make sure no one is changing ouw data mid-way through oru printing operation
        lock ( frontDoor )
        {
            //Print out the student ID here, safe in the knowledfe that nothing is messing with our data
        }

    }

    private void SlowJob()
    {
        Debug.Log( "Doing 1000 things, each taking 2ms" );
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        //In a separete thread, decidimos cambiar el contents of FOO
        lock ( frontDoor )  //if frontDoor is already locked, the thread will pause until frontDoor is not locked.
        {
            foo = "English Literature";
        }
        

        for ( int i = 0; i < 1000; i++ )
        {
            //this.transform.Translate( Vector3.up * 0.002f );    //No funciona ya que es algo de unity en otro hilo que no es el principal.
            Thread.Sleep( 2 );  //SleepTimeout 2ms
        }

        //Note: Porque son varios overheads el tiempo transcurrido será poco más de 2 segundos
        Debug.Log( "Done! Elapsed Time: " + sw.ElapsedMilliseconds / 1000f );
    }

}
