using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{  
    public static List<Jugador> jugadores = new List<Jugador>();
    public static Random random = new Random();
    public static char[,] mapa;
    public static int mapaAncho = 20;
    public static int mapaAlto = 20;
    public static (int, int) posicionMeta;
    public static List<(int, int)> posicionesCartasMagicas = new List<(int, int)>();
    static int cantidadCartasMagicas = 4;
    static List<(int, int)> posicionesObstaculos = new List<(int, int)>();
    static int cantidadObstaculos = 40;

    static void Main()
    {
        try
        {
            Historia();
            Instrucciones();
            List<Cartas> cartas = EstablecerCartas();
            const int cantidadJugadores = 2;
            int conteoCartas = CantidadDeCartas();

            for (int i = 0; i < cantidadJugadores; i++)
            {
                string nombreJugador;
                bool nombreValido;
                do
                {
                    Console.Write($"Ingrese el nombre del Jugador {i + 1}: ");
                    nombreJugador = Console.ReadLine()!;
                    nombreValido = !string.IsNullOrEmpty(nombreJugador); 

                    if (nombreValido)
                    {
                         
                        foreach (var jugadorExistente in jugadores)
                        {
                            if (jugadorExistente.Nombre == nombreJugador)
                            {
                                Console.WriteLine("No puedes usar este nombre, ya que corresponde al otro jugador. Por favor, ingrese un nombre diferente.");
                                nombreValido = false; 
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("El nombre no puede estar vacío. Por favor, ingrese un nombre.");
                    }
                }
                while (!nombreValido); 

                List<Cartas> ObtenerTodasLasCartas()
                {
                    List<Cartas> todasLasCartas = new List<Cartas>();
                    foreach (var jugador in jugadores)
                    {
                        todasLasCartas.AddRange(jugador.Cartas);
                    }
                    return todasLasCartas;
                }

                var cartasDisponibles = cartas.Except(ObtenerTodasLasCartas()).ToList();
                var cartasJugador = SeleccionarCartas(cartasDisponibles.ToList(), conteoCartas);
                jugadores.Add(new Jugador(nombreJugador, cartasJugador));

                if (i == 0)
                {
                    Console.Clear();
                }
            }

            mapa = GenerarMapa(mapaAncho, mapaAlto);
            ActualizarEnfriamientos();
            Jugar(mapa);
        }
        catch (Exception ex)
        {
            ManejarExcepcion(ex);
        }
    }

    static void ManejarExcepcion(Exception ex)
    {
        Console.WriteLine($"Se produjo un error: {ex.Message}");
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void Historia()
    {
        int tiempoEspera = 15;
        Console.WriteLine("\nHace mucho tiempo cuando las pirámides aún eran jóvenes...\nLos reyes egipcios jugaban un juego de un gran y terrible poder. \nPero estos juegos de las sombras se convirtieron en una guerra que amenazaba con destruir a todo el mundo \n hasta que un valiente y poderoso faraon encerro la magia apresandola dentro de los misticos articulos del milenio. \nAhora 5000 años despues un joven llamado Yugi decifra el secreto del rompecabezas del milenio, posee energia magica antigua. \nPues el destino lo eligio para defender al mundo del regreso de los juegos de las sombras al igual que lo hizo el valiente faraon hace 5000 años");
        Console.WriteLine("Bienvenidos duelistas, hoy da inicio al torneo de Ciudad Batallas, espero que escojan bien sus cartas y así poder avanzar. \n Recuerden que deben luchar con honor y sabiduría, sin juegos sucios o trampas. \n Cuando el torneo termine aquel que llegue al Coliseo será reconocido como el mejor jugador, y será recordado en la historia como \"El rey de los juegos\". \nDisfruten del torneo y que el mejor gane.");

        for (int i = tiempoEspera; i > 0; i--)
        {
            Console.Write($"\rPor favor, espera {i} segundos para continuar.");
            Thread.Sleep(1000);
        }

        Console.WriteLine();
        Console.Clear();
    }

    static void Instrucciones()
    {
        int tiempoEspera = 15;
        Console.WriteLine("Instrucciones:");
        Console.WriteLine("Bienvenido duelista. En esta aventura tendrás que moverte alrededor del mapa hasta llegar al Coliseo(C) y ser reconocido como el 'Rey de los Juegos'.");
        Console.WriteLine("Para jugar este juego deberás escoger inteligentemente tus cartas teniendo en cuenta sus habilidades y la cantidad de pasos que cada una avanza.");
        Console.WriteLine("Las cartas del Primer jugador se mostrarán como (P) y las del Segundo jugador como (S) en el mapa.");
        Console.WriteLine("Luego se generará el mapa de la ciudad en donde aparecerán varias trampas(T) y otros duelistas(D) que harán más difícil tu camino.");
        Console.WriteLine("Asimismo aparecerán cartas mágicas(M) que harán un poco más fácil (o no) tu camino hacia la victoria.");
        Console.WriteLine("Para avanzar por la ciudad solo debes pulsar la flecha respectiva a la dirección que quieras tomar.");

        for (int i = tiempoEspera; i > 0; i--)
        {
            Console.Write($"\rListo, ya estás preparado para comenzar. Espera {i} segundos para continuar.");
            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }
    static char[,] GenerarMapa(int ancho, int alto)
    {
        char[,] mapa = new char[ancho, alto];

        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                mapa[i, j] = '_';
            }
        }

        int metaX = random.Next(ancho);
        int metaY = random.Next(alto);
        mapa[metaX, metaY] = 'C';
        posicionMeta = (metaX, metaY);

        int cantidadTrampas = 5;
        for (int i = 0; i < cantidadTrampas; i++)
        {
            int trampaX = random.Next(ancho);
            int trampaY = random.Next(alto);

            if (trampaX != metaX || trampaY != metaY)
            {
                mapa[trampaX, trampaY] = 'T';
            }
        }
        
        posicionesCartasMagicas.Clear();
        for (int i = 0; i < cantidadCartasMagicas; i++)
        {
            int cartaMagicaX = random.Next(ancho);
            int cartaMagicaY = random.Next(alto);

            
            if ((cartaMagicaX != metaX || cartaMagicaY != metaY) && mapa[cartaMagicaX, cartaMagicaY] != 'T')
            {
                posicionesCartasMagicas.Add((cartaMagicaX, cartaMagicaY));
                
            }
            else
            {
                i--; 
            }
        }

        
        posicionesObstaculos.Clear();
        for (int i = 0; i < cantidadObstaculos; i++)
        {
            int obstaculoX = random.Next(ancho);
            int obstaculoY = random.Next(alto);

            
            if ((obstaculoX != metaX || obstaculoY != metaY) && mapa[obstaculoX, obstaculoY] != 'T' && !posicionesCartasMagicas.Contains((obstaculoX, obstaculoY)))
            {
                if (!posicionesObstaculos.Contains((obstaculoX, obstaculoY)))
                {
                    posicionesObstaculos.Add((obstaculoX, obstaculoY));
                }
                else
                {
                    i--; 
                }
            }
            else
            {
                i--; 
            }
        }

        return mapa;
    }

    static void MostrarMapa(char[,] mapa)
    {
        Console.Clear(); 
        Console.WriteLine("Mapa del juego:");
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                char celda = mapa[i, j];

                
                if (i == posicionMeta.Item1 && j == posicionMeta.Item2)
                {
                    celda = 'C';
                }
                
                else if (mapa[i, j] == 'T')
                {
                    celda = 'T';
                }
                
                 else if (posicionesObstaculos.Contains((i, j)))
                {
                    celda = 'D';
                }
                
                else if (posicionesCartasMagicas.Contains((i, j)))
                {
                    celda = 'M';
                }
                
                else
                {
                    foreach (var jugador in jugadores)
                    {
                        foreach (var carta in jugador.Cartas)
                        {
                            if (carta.X == i && carta.Y == j)
                            {
                                celda = (jugadores.IndexOf(jugador) == 0) ? 'P' : 'S';
                                break;
                            }
                        }
                    }
                }
                Console.Write(celda + " ");
            }
            Console.WriteLine();
        }
    }

    static void Jugar(char[,] mapa)
    {
        try
        {
            MostrarMapa(mapa);
            while (true)
            {
                foreach (var jugador in jugadores)
                {
                    if (CartasAturdidas(jugador.Cartas))
                    {
                        Console.WriteLine($"{jugador.Nombre} no podrá jugar en este turno. Todas sus cartas tienen aturdimiento.");
                        continue;
                    }

                    string simboloFicha = (jugadores.IndexOf(jugador) == 0) ? "P" : "S";
                    Console.WriteLine($"\nTurno de {jugador.Nombre} ({simboloFicha})");

                    SeleccionarTurnoJugador(jugador.Cartas, mapa);

                                        bool llegoALaMeta = false;
                    foreach (var carta in jugador.Cartas)
                    {
                        if (carta.X == posicionMeta.Item1 && carta.Y == posicionMeta.Item2)
                        {
                             
                            llegoALaMeta = true;
                            break;
                        }
                    }

                    if (llegoALaMeta)
                    {
                        Console.WriteLine($"¡{jugador.Nombre} ha llegado a la meta y ha ganado!");
                       
                        Console.ReadLine();
                        return;
                    }
                    
                    
                }
                ActualizarEnfriamientos();
                Thread.Sleep(1000);
            }
        }
        catch (Exception ex)
        {
            ManejarExcepcion(ex);
        }
    }

    static int CantidadDeCartas()
    {
        int cantidadCartas = 0;
        bool entradaValida = false;

        while (!entradaValida)
        {
            try
            {
                Console.WriteLine("¿Con cuántas cartas desean jugar? (1 a 3):");
                string entrada = Console.ReadLine()!;

                if (int.TryParse(entrada, out cantidadCartas) && cantidadCartas >= 1 && cantidadCartas <= 3)
                {
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine("Por favor, ingresa un número válido entre 1 y 3.");
                }
            }
            catch (Exception ex)
            {
                ManejarExcepcion(ex);
            }
        }

        return cantidadCartas;
    }

    static void ActualizarEnfriamientos()
    {
        foreach (var jugador in jugadores)
        {
            foreach (var carta in jugador.Cartas)
            {
                carta.ActualizarEnfriamiento();

                if (carta.TurnosRestantesReduccionVelocidad > 0)
                {
                    carta.TurnosRestantesReduccionVelocidad--;
                }
                else if (carta.Monstruo.Velocidad < carta.VelocidadOriginal)
                {
                    carta.Monstruo.Velocidad += 1;
                }

                if (carta.TurnosStunRestantes > 0)
                {
                    carta.TurnosStunRestantes--;
                    if (carta.TurnosStunRestantes == 0)
                        carta.SePuedeMover = true;
                }
                else
                {
                    carta.SePuedeMover = true;
                }
            }
        }
    }

    static bool CartasAturdidas(List<Cartas> cartas)
    {
        foreach (var carta in cartas)
        {
            if (carta.SePuedeMover)
            {
                return false; 
            }
        }
        return true; 
    }

static void SeleccionarTurnoJugador(List<Cartas> cartas, char[,] mapa)
{
    Console.WriteLine("Seleccione la carta que desea mover:");
    MostrarCartasConDetalles(cartas);
    int indiceEleccion;

    while (true)
    {
        string n = Console.ReadLine();
        if (!int.TryParse(n, out indiceEleccion) || indiceEleccion < 1 || indiceEleccion > cartas.Count)
        {
            Console.WriteLine("Selección no válida. Intente nuevamente.");
            continue;
        }

        Cartas cartaSeleccionada = cartas[indiceEleccion - 1];

        if (!cartaSeleccionada.SePuedeMover)
        {
            Console.WriteLine($"{cartaSeleccionada.Monstruo.Nombre} no se moverá este turno porque está aturdido, selecciona otra carta.");
            continue;
        }

        
        bool turnoCompletado = false; 
        while (!turnoCompletado)
        {
            Console.WriteLine("¿Qué desea hacer con esta carta?");
            if (cartaSeleccionada.PuedeUsarHabilidad)
            {
                Console.WriteLine("1. Usar Habilidad");
            }
            Console.WriteLine("2. Mover");
            Console.Write("Ingrese su elección (1 o 2): ");

            string opcionElegida = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opcionElegida)) 
            {
                Console.WriteLine("Opción no válida. Por favor, elija una opción correcta.");
                continue;
            }

            if (opcionElegida == "1" && cartaSeleccionada.PuedeUsarHabilidad)
            {
                
                ActivarHabilidad(cartaSeleccionada);

                
                Console.WriteLine("Ahora puedes mover esta carta.");
                TurnoDelJugador(cartaSeleccionada, mapa);
                turnoCompletado = true; 
            }
            else if (opcionElegida == "2")
            {
                
                TurnoDelJugador(cartaSeleccionada, mapa);
                turnoCompletado = true; 
            }
            else if (opcionElegida == "1" && !cartaSeleccionada.PuedeUsarHabilidad)
            {
                Console.WriteLine($"{cartaSeleccionada.Monstruo.Nombre} no puede usar su habilidad especial este turno debido a enfriamiento.");
                continue;
            }
            else
            {
                Console.WriteLine("Opción no válida. Por favor, elija una opción correcta.");
                continue;
            }
        }
        break; 
    }
}


    private static void ActivarHabilidad(Cartas cartaSeleccionada)
    {
        Console.WriteLine($"¿{cartaSeleccionada.Monstruo.Nombre} quiere usar su habilidad especial? (1 para Sí/ 2 para No)");

        int usoHabilidad;

        while (true)
        {
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out usoHabilidad))
            {
                if (usoHabilidad == 1)
                {
                    List<Cartas> cartasEnemigas = ObtenerCartasEnemigas(ObtenerTodasLasCartas(), cartaSeleccionada);
                    cartaSeleccionada.ActivarHabilidad(cartasEnemigas);
                    break;
                }
                else if (usoHabilidad == 2)
                {
                    Console.WriteLine($"{cartaSeleccionada.Monstruo.Nombre} no usará su habilidad especial este turno.");
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Por favor, ingrese 1 o 2.");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida. Por favor ingrese un número (1 o 2).");
            }
        }
    }

    static List<Cartas> ObtenerTodasLasCartas()
    {
        List<Cartas> todasLasCartas = new List<Cartas>();
        foreach (var jugador in jugadores)
        {
            todasLasCartas.AddRange(jugador.Cartas);
        }
        return todasLasCartas;
    }
    

    static void MostrarCartasConDetalles(List<Cartas> cartas)
    {
        try
        {
            Console.WriteLine("Opciones disponibles:");
            for (int i = 0; i < cartas.Count; i++)
            {
                var carta = cartas[i];
                Console.WriteLine($"{i + 1}  {carta.Monstruo.Nombre} | Velocidad: {carta.Monstruo.Velocidad} | Tiempo de enfriamiento: {carta.TiempoEnfriamientoRestante} | Habilidad: {carta.Monstruo.Habilidad} | Localización: ({carta.X}, {carta.Y})");
            }
        }
        catch (Exception ex) { ManejarExcepcion(ex); }
    }

    static List<Cartas> ObtenerCartasEnemigas(List<Cartas> todasLasCartasDelJuego, Cartas cartaActual)
{
    List<Cartas> cartasEnemigas = new List<Cartas>();
    foreach (var carta in todasLasCartasDelJuego)
    {
        if (carta != cartaActual)
        {
            cartasEnemigas.Add(carta);
        }
    }
    return cartasEnemigas;
}

    static void TurnoDelJugador(Cartas carta, char[,] mapa)
    {
        if (carta.SeProtege && carta.TurnosDeProteccion > 0)
        {
            carta.TurnosDeProteccion--;
            if (carta.TurnosDeProteccion == 0)
                carta.SeProtege = false;

            Console.WriteLine(carta.SeProtege ? $"{carta.Monstruo.Nombre} sigue protegido." : $"{carta.Monstruo.Nombre} ya no está protegido.");
        }

        if (!carta.SePuedeMover)
       {
           Console.WriteLine($"{carta.Monstruo.Nombre} no puede moverse este turno.");
            carta.SePuedeMover = true;
            return;
        }

        Console.WriteLine($"Turno de {carta.Monstruo.Nombre}. Posición actual: ({carta.X}, {carta.Y})");

        MoverCarta(carta, mapa);
    }

    static void MoverCarta(Cartas carta, char[,] mapa)
{
    int movimientosRestantes = carta.Monstruo.Velocidad;
    bool ignorarTrampa = carta.SeIgnoraTrampaEsteTurno;

    while (movimientosRestantes > 0)
    {
        
        if (carta.X == posicionMeta.Item1 && carta.Y == posicionMeta.Item2)
        {
            Console.WriteLine($"{carta.Monstruo.Nombre} ha llegado a la meta y no puede moverse más.");
            movimientosRestantes = 0;

            
            string nombreJugador = "";
            foreach (var jugador in Program.jugadores)
            {
                if (jugador.Cartas.Contains(carta))
                {
                    nombreJugador = jugador.Nombre;
                    break;
                }
            }

            Console.WriteLine($"¡{nombreJugador} ha ganado el juego!");
            return;
        }

        Console.WriteLine($"Movimientos restantes: {movimientosRestantes}");
        Console.WriteLine("Utiliza las flechas del teclado para mover (↑, ↓, ←, →) o presiona 'C' para cancelar:");

        
        ConsoleKeyInfo keyInfo = Console.ReadKey(true); 
        int nuevaX = carta.X;
        int nuevaY = carta.Y;

        
        if (keyInfo.Key == ConsoleKey.UpArrow) 
        {
            nuevaX--;
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow) 
        {
            nuevaX++;
        }
        else if (keyInfo.Key == ConsoleKey.LeftArrow) 
        {
            nuevaY--;
        }
        else if (keyInfo.Key == ConsoleKey.RightArrow) 
        {
            nuevaY++;
        }
        else if (keyInfo.Key == ConsoleKey.C) 
        {
            Console.WriteLine("Movimiento cancelado.");
            return;
        }
        else
        {
            Console.WriteLine("Tecla no válida. Usa las flechas del teclado para moverte.");
            continue;
        }

        
        if (nuevaX >= 0 && nuevaX < mapaAncho && nuevaY >= 0 && nuevaY < mapaAlto)
        {
            if (posicionesObstaculos.Contains((nuevaX, nuevaY)))
            {
                Console.WriteLine($"{carta.Monstruo.Nombre} ha chocado con un duelista. Intenta moverte en otra dirección.");
                continue;
            }

            
            if (mapa[nuevaX, nuevaY] == 'T' && !ignorarTrampa)
            {
                Console.WriteLine($"{carta.Monstruo.Nombre} ha caído en una trampa.");
                ActivarTrampa(carta);
                return; 
            }
            else
            {
                if (ignorarTrampa)
                {
                    Console.WriteLine($"{carta.Monstruo.Nombre} ha evitado una trampa gracias a su habilidad.");
                }

                
                carta.Mover(nuevaX, nuevaY);
                movimientosRestantes--;

                
                MostrarMapa(mapa);

                
                if (carta.X == posicionMeta.Item1 && carta.Y == posicionMeta.Item2)
                {
                    Console.WriteLine($"{carta.Monstruo.Nombre} ha llegado a la meta y no puede moverse más.");
                    movimientosRestantes = 0;

                    string nombreJugador = "";
                    foreach (var jugador in Program.jugadores)
                    {
                        if (jugador.Cartas.Contains(carta))
                        {
                            nombreJugador = jugador.Nombre;
                            break;
                        }
                    }

                    Console.WriteLine($"¡{nombreJugador} ha ganado el juego!");
                    return;
                }

                
                if (posicionesCartasMagicas.Contains((nuevaX, nuevaY)))
                {
                    Console.WriteLine($"{carta.Monstruo.Nombre} ha encontrado una carta mágica.");
                    carta.CartasMagicas(carta);
                    posicionesCartasMagicas.Remove((nuevaX, nuevaY));
                    mapa[nuevaX, nuevaY] = '_'; 
                }
            }
        }
        else
        {
            Console.WriteLine("No puedes salirte del mapa. Intenta otra dirección.");
        }
    }

    
    carta.SeIgnoraTrampaEsteTurno = false;
}

    static void ActivarTrampa(Cartas carta)
    {
        int tipoDeTrampa = random.Next(1, 6);

        if (tipoDeTrampa == 1)
        {
         Console.Write($"{carta.Monstruo.Nombre} ha caído en Agujero Trampa y ha sido devuelto a su posición inicial.");
            carta.Mover(carta.XDeInicio, carta.YDeInicio);
        }
        else if (tipoDeTrampa == 2)
        {
            Console.Write($"{carta.Monstruo.Nombre} ha sido lanzado el Dado Calavera y su velocidad se ha reducido en uno durante dos turnos.");
            carta.DisminuirVelocidad(1);
            carta.TurnosRestantesReduccionVelocidad = 2;
        }
                        else if (tipoDeTrampa == 3)
        {
            Console.Write($"{carta.Monstruo.Nombre} se ha encontrado con Espantapájaros de Chatarra y ha sido aturdido por tres turnos.");
            carta.SePuedeMover = false;
            carta.TurnosStunRestantes = 3;
        }
        else if (tipoDeTrampa == 4)
        {
            Console.Write($"{carta.Monstruo.Nombre} ha pisado un Círculo Mágico y ha sido aturdido por dos turnos.");
            carta.SePuedeMover = false;
            carta.TurnosStunRestantes = 2;
        }
        else if (tipoDeTrampa == 5)
        {
            Console.Write($"{carta.Monstruo.Nombre} ha sido impactado por Fuerza de Espejo. Regresa a la posición anterior.");
            carta.RegresarAPosicionAnterior();
        }
    }
static List<Cartas> EstablecerCartas()
    {
        var personajes = new List<Monstruo>
       {
           new Monstruo { Nombre = "Mago Oscuro", Habilidad = "Ataque de Magia Oscura: Stunea dos turnos a una carta.", Velocidad = 2, TiempoEnfriamiento = 4 },
           new Monstruo { Nombre = "Héroe Elemental Neos", Habilidad = "Quita la protección contra stun de una carta", Velocidad = 2, TiempoEnfriamiento = 3 },
           new Monstruo { Nombre = "Dragón Polvo de Estrellas", Habilidad = "Polvo Estelar: Disminuye la velocidad de todas las cartas enemigas", Velocidad = 3, TiempoEnfriamiento = 4 },
           new Monstruo { Nombre = "Dragón Péndulo de Ojos Anómalos", Habilidad = "Ojos Anómalos: Stunear a una carta por dos turnos.", Velocidad = 2, TiempoEnfriamiento = 4 },
           new Monstruo { Nombre = "Decode Talker", Habilidad = "Decodificar: Avanzar dos pasos más.", Velocidad = 1, TiempoEnfriamiento = 5 },
           new Monstruo { Nombre = "Dragón Sincro de Ala Cristalina", Habilidad = "Niega la activación de la habilidad de una ficha temporalmente.", Velocidad = 3, TiempoEnfriamiento = 6 },
           new Monstruo { Nombre = "Archfiend Black Skull Dragon", Habilidad = "Rugido Archidemonio: Cuando avanza, si se topa con una trampa, esta no le afecta y la atraviesa.", Velocidad = 3, TiempoEnfriamiento = 3 },
           new Monstruo { Nombre = "Dragón XYZ de Rebelión Oscura", Habilidad = "Rebelión Oscura: Stunea 2 turnos a una carta.", Velocidad = 2, TiempoEnfriamiento = 4 },
           new Monstruo { Nombre = "Dragón Sincro de Ala Transparente", Habilidad = "Disminuye la velocidad a una carta.", Velocidad = 3, TiempoEnfriamiento = 4 }
       };

            List<Cartas> cartas = new List<Cartas>();
    foreach (var monstruo in personajes)
    {
        Cartas nuevaCarta = new Cartas(monstruo);
        cartas.Add(nuevaCarta);
    }
    return cartas;
    }

    
        static List<Cartas> SeleccionarCartas(List<Cartas> cartasParaSeleccionar, int cantidad)
    {
        var cartasSeleccionadas = new List<Cartas>();

        while (cartasSeleccionadas.Count < cantidad && cartasParaSeleccionar.Count > 0)
        {
            MostrarOpciones(cartasParaSeleccionar);
            int indiceEleccion;

            while (true)
            {
                Console.WriteLine("Ingrese el número de la carta que desea escoger: ");
                if (int.TryParse(Console.ReadLine(), out indiceEleccion) && indiceEleccion >= 1 && indiceEleccion <= cartasParaSeleccionar.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Selección no válida. Inténtalo de nuevo.");
                }
            }

            cartasSeleccionadas.Add(cartasParaSeleccionar[indiceEleccion - 1]);
            cartasParaSeleccionar.RemoveAt(indiceEleccion - 1);
        }

        return cartasSeleccionadas;
    }
static void MostrarOpciones(List<Cartas> cartas)
    {
        Console.WriteLine("Opciones disponibles:");
        for (int i = 0; i < cartas.Count; i++)
        {
            Console.WriteLine($"{i + 1} => {cartas[i].Monstruo.Nombre} | Velocidad: {cartas[i].Monstruo.Velocidad} | Tiempo de enfriamiento: {cartas[i].Monstruo.TiempoEnfriamiento} | Habilidad: {cartas[i].Monstruo.Habilidad}");
        }
    }
}

class Cartas
{
    Random random = new Random();
    public Monstruo Monstruo { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool PuedeUsarHabilidad
{
    get { return TiempoEnfriamientoRestante <= 0; }
}

    public bool SePuedeMover { get; set; }
    public int TiempoEnfriamientoRestante { get; set; }
    public bool SeProtege { get; set; }
    public int TurnosDeProteccion { get; set; }
    public int XDeInicio { get; private set; }
    public int YDeInicio { get; private set; }
    private int XAnterior { get; set; }
    private int YAnterior { get; set; }
    public int TurnosRestantesReduccionVelocidad { get; set; }
    public int TurnosStunRestantes { get; set; }
    public int VelocidadOriginal { get; private set; }
    public bool SeIgnoraTrampaEsteTurno { get; set; } = false;

    public Cartas(Monstruo monstruo)
    {
        Monstruo = monstruo;
        XDeInicio = random.Next(Program.mapaAncho);
        YDeInicio = random.Next(Program.mapaAlto);
        X = XDeInicio;
        Y = YDeInicio;
        SePuedeMover = true;
        TiempoEnfriamientoRestante = 0;
        SeProtege = false;
        TurnosDeProteccion = 0;
        TurnosRestantesReduccionVelocidad = 0;
        TurnosStunRestantes = 0;
        VelocidadOriginal = Monstruo.Velocidad;
        XAnterior = X;
        YAnterior = Y;
    }

    public void Mover(int x, int y)
    {
        XAnterior = X;
        YAnterior = Y;
        X = x;
        Y = y;
    }

    public void RegresarAPosicionAnterior()
    {
        X = XAnterior;
        Y = YAnterior;
    }

    public void ActualizarEnfriamiento()
    {
        if (TiempoEnfriamientoRestante > 0)
            TiempoEnfriamientoRestante--;
    }

    public void DisminuirVelocidad(int cantidad)
    {
        Monstruo.Velocidad -= cantidad;

        if (Monstruo.Velocidad < 1)
            Monstruo.Velocidad = 1;
    }

    public void ActivarHabilidad(List<Cartas> cartasEnemigas)
    {
        if (Monstruo.Nombre == "Archfiend Black Skull Dragon")
        {
            Console.WriteLine($"{Monstruo.Nombre} ha activado Rugido Archidemonio. Ignorará trampas al moverse durante este turno.");
            SeIgnoraTrampaEsteTurno = true;
        }
        else if (Monstruo.Nombre == "Mago Oscuro")
        {
            StunEnemigo(cartasEnemigas);
        }
        else if (Monstruo.Nombre == "Héroe Elemental Neos")
        {
            QuitarBuffsEnemigo(cartasEnemigas);
        }

        else if (Monstruo.Nombre == "Dragón Polvo de Estrellas")
{
    
    Jugador jugadorActual = null;
    foreach (var jugador in Program.jugadores)
    {
        if (jugador.Cartas.Contains(this))
        {
            jugadorActual = jugador;
            break; 
        }
    }

    if (jugadorActual != null)
    {
        
        foreach (var jugador in Program.jugadores)
        {
            
            if (jugador != jugadorActual)
            {
                
                foreach (var carta in jugador.Cartas)
                {
                    carta.DisminuirVelocidad(1);
                    Console.WriteLine($"{Monstruo.Nombre} ha disminuido la velocidad de {carta.Monstruo.Nombre} en 1.");
                }
            }
        }
    }
}

        else if (Monstruo.Nombre == "Dragón Péndulo de Ojos Anómalos")
        {
            StunEnemigo(cartasEnemigas);
        }
        else if (Monstruo.Nombre == "Decode Talker")
        {
            AumentarVelocidad(2);
            Console.WriteLine($"{Monstruo.Nombre} ha aumentado su velocidad en dos pasos.");
        }
        else if (Monstruo.Nombre == "Dragón Sincro de Ala Cristalina")
        {
            NegarHabilidadEnemigo(cartasEnemigas);
            Console.WriteLine($"{Monstruo.Nombre} ha negado la habilidad especial de un enemigo.");
        }
        else if (Monstruo.Nombre == "Dragón XYZ de Rebelión Oscura")
        {
            StunEnemigo(cartasEnemigas);
            Console.WriteLine($"{Monstruo.Nombre} ha aturdido a un enemigo por dos turnos.");
        }
        else if (Monstruo.Nombre == "Dragón Sincro de Ala Transparente")
{
    var enemigo = cartasEnemigas[random.Next(cartasEnemigas.Count)];
    enemigo.DisminuirVelocidad(1);
    Console.WriteLine($"{Monstruo.Nombre} ha disminuido la velocidad de {enemigo.Monstruo.Nombre} en 1.");
}


        TiempoEnfriamientoRestante = Monstruo.TiempoEnfriamiento;
    }

    private void StunEnemigo(List<Cartas> cartasEnemigas)
{
    
    Jugador jugadorActual = null;
    foreach (var jugador in Program.jugadores)
    {
        if (jugador.Cartas.Contains(this))
        {
            jugadorActual = jugador;
            break; 
        }
    }

    if (jugadorActual != null)
    {
        
        List<Cartas> enemigosReales = cartasEnemigas.Where(carta => !jugadorActual.Cartas.Contains(carta)).ToList();

        
        if (enemigosReales.Count > 0)
        {
            var enemigo = enemigosReales[random.Next(enemigosReales.Count)];
            enemigo.TurnosStunRestantes += 2;
            enemigo.SePuedeMover = false;

            Console.WriteLine($"{Monstruo.Nombre} ha aturdido a {enemigo.Monstruo.Nombre} por dos turnos.");
        }
        else
        {
            Console.WriteLine($"{Monstruo.Nombre} no encontró enemigos para aturdir.");
        }
    }
}


    private void QuitarBuffsEnemigo(List<Cartas> cartasEnemigas)
    {
        foreach (var enemigo in cartasEnemigas)
        {
            if (enemigo.SeProtege)
            {
                enemigo.SeProtege = false;

                Console.WriteLine($"{Monstruo.Nombre} HA QUITADO LA PROTECCIÓN DE {enemigo.Monstruo.Nombre}");
            }
        }

        Console.WriteLine($"{Monstruo.Nombre} HA QUITADO BUFFS A LOS ENEMIGOS.");
    }

    private void NegarHabilidadEnemigo(List<Cartas> cartasEnemigas)
    {
        var enemigo = cartasEnemigas[random.Next(cartasEnemigas.Count)];
        enemigo.TurnosStunRestantes += 1;

        Console.WriteLine($"{enemigo.Monstruo.Nombre} NO PUEDE USAR SU HABILIDAD ESPECIAL ESTE TURNO.");
    }

    public void CartasMagicas(Cartas carta)
    {
        Random randomGen = new Random();
        int tipoCartaMagica = randomGen.Next(1, 5);

        if (tipoCartaMagica == 1)
        {
            int aumentoDeVelocidad = randomGen.Next(1, 3);
            AumentarVelocidad(aumentoDeVelocidad);

            Console.WriteLine($"{Monstruo.Nombre} HA SIDO LANZADO EL DADO DE GRACIA Y HA AUMENTADO SU VELOCIDAD EN {aumentoDeVelocidad} TURNOS");
        }
        else if (tipoCartaMagica == 2)
        {
            ActivarTeletransporte();

            Console.WriteLine($"{Monstruo.Nombre} HA REDUCIDO SU TIEMPO DE ENFRIAMIENTO EN UNO TURNOS.");
        }
        else if (tipoCartaMagica == 3)
        {
            SeProtege = true;
            TurnosDeProteccion = 3;

            Console.WriteLine($"{Monstruo.Nombre} HA ENCONTRADO LA CARTA: ESPADAS DE LA LUZ REVELADORA Y AHORA ESTÁ PROTEGIDO CONTRA ATAQUES (ATURDIMIENTO) DURANTE {TurnosDeProteccion} TURNOS.");
        }
        else if (tipoCartaMagica == 4)
        {
            ActivarAgujeroNegro();
        }
        else
        {
            Console.WriteLine("NO SE HA APLICADO NINGÚN BENEFICIO.");
        }
    }

    private void ActivarTeletransporte()
    {
        Console.WriteLine($"{Monstruo.Nombre} HA ACTIVADO TELETRANSPORTE. Teletransportando a una nueva posición aleatoria...");

        int nuevaX, nuevaY;
        bool posicionEncontrada = false;

        for (int i = 0; i < 100; i++)
        {
            nuevaX = random.Next(Program.mapaAncho);
            nuevaY = random.Next(Program.mapaAlto);

            if (Program.mapa[nuevaX, nuevaY] == '_')
            {
                Mover(nuevaX, nuevaY);
                posicionEncontrada = true;
                Console.WriteLine($"{Monstruo.Nombre} se ha teletransportado a la posición ({nuevaX},{nuevaY}).");
                break;
            }
        }

        if (!posicionEncontrada)
        {
            Console.WriteLine("No se encontró una posición vacía para teletransportarse.");
        }
    }

    public void AumentarVelocidad(int cantidad)
{
    Monstruo.Velocidad += cantidad;
}


    private void ActivarAgujeroNegro()
    {
        Console.WriteLine($"{Monstruo.Nombre} HA ACTIVADO AGUJERO NEGRO. TODAS LAS CARTAS REGRESARÁN A SUS POSICIONES INICIALES.");

        foreach (var jugadorCartas in Program.jugadores)
        {
            foreach (var carta in jugadorCartas.Cartas)
            {
                carta.Mover(carta.XDeInicio, carta.YDeInicio);
            }
        }

        Console.WriteLine("TODAS LAS CARTAS HAN REGRESADO A SUS POSICIONES INICIALES.");
    }
}

class Jugador
{
    public string Nombre { get; private set; }
    public List<Cartas> Cartas { get; private set; }

    public Jugador(string nombre, List<Cartas> cartas)
    {
        Nombre = nombre;
        Cartas = cartas;
    }
}

class Monstruo
{
    public string Nombre { get; set; }
    public int Velocidad { get; set; }
    public int TiempoEnfriamiento { get; set; }
    public string Habilidad { get; set; }
}

      