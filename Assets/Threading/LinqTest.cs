using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqTest : MonoBehaviour
{
    List<player> usuarios = new List<player> {
        new player("Alan", "Stich", 26),
        new player("Gaby", "Stich", 22),
        new player("Tatiana", "tía", 22),
        new player("Eve", "Chismes", 26),
        new player("Vane", "wii", 26)
    };

    private void Start()
    {
        var playersAgeDD = usuarios.Where( u => u.edad == 22 ).ToList();
        var playersAgeDS = usuarios.First( u => u.edad == 26 );
        Debug.Log( playersAgeDS.nombre );
    }

}

public class player
{
    public string nombre;
    public string alias;
    public int edad;

    public player( string _nombre, string _alias, int _edad )
    {
        this.nombre = _nombre;
        this.alias = _alias;
        this.edad = _edad;
    }

}