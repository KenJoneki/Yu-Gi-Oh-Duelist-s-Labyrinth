Informe Completo sobre Yu-Gi-Oh! Duelist's Labyrinth:
Introducción
¡Bienvenidos al emocionante mundo del juego de cartas interactivo inspirado en el universo de Yu-Gi-Oh!! En este juego, los duelistas se enfrentan en un mapa lleno de trampas, duelistas rivales y cartas mágicas, todo mientras compiten por alcanzar el Coliseo y ser coronados como "El Rey de los Juegos". Prepárate para una aventura llena de estrategia, decisiones inteligentes y sorpresas en cada turno.
Características Destacadas
1. Jugabilidad Dinámica
Turnos Estratégicos: Cada jugador toma decisiones en turnos, eligiendo qué carta mover y cuándo usar habilidades especiales.
Interacción con el Mapa: El mapa está lleno de trampas y elementos que pueden ayudar o dificultar tu progreso.
2. Diversidad de Cartas
Monstruos Únicos: Cada carta representa un monstruo con habilidades especiales que pueden cambiar el rumbo del juego.
Habilidades Especiales: Usa habilidades estratégicas para aturdir a tus oponentes, aumentar tu velocidad o protegerte de ataques.
3. Mapa Aleatorio
Generación Dinámica: Cada partida presenta un mapa único con diferentes ubicaciones para trampas, duelistas y cartas mágicas.
Desafíos Diversos: Enfréntate a diferentes situaciones en cada partida, lo que garantiza que cada juego sea una experiencia nueva.
4. Modo Multijugador
Competencia entre Amigos: Juega con hasta dos jugadores y demuestra quién es el mejor duelista.
Estrategia y Tácticas: Utiliza tus cartas sabiamente para superar a tu oponente y alcanzar la victoria.
Instrucciones de Instalación
¡Es fácil comenzar tu aventura! Sigue estos simples pasos para instalar y ejecutar el juego:
Instala .NET SDK:
Asegúrate de tener instalado el SDK de .NET en tu computadora.
Crea un Nuevo Proyecto:
Abre Visual Studio o Visual Studio Code.
Selecciona "Crear un nuevo proyecto" y elige "Aplicación de consola (.NET Core)".
Copia el Código:
Pega el código proporcionado en el archivo Program.cs del proyecto.
Ejecuta el Proyecto:
Compila y ejecuta el proyecto desde tu entorno de desarrollo. ¡Estás listo para jugar!
¿De Qué Trata el Juego?
En este juego, los jugadores asumen el rol de duelistas que deben mover sus cartas a través de un mapa lleno de desafíos. La historia se desarrolla en un antiguo Egipto donde los reyes jugaban a juegos poderosos que amenazaban al mundo. Ahora, 5000 años después, tú eres uno de esos duelistas que debe enfrentarse a peligros y trampas para llegar al Coliseo.
Objetivo del Juego
El objetivo es simple pero desafiante: mueve tus cartas por el mapa, evita trampas, interactúa con otros duelistas y recoge cartas mágicas. El primer jugador en llegar al Coliseo (marcado como 'C' en el mapa) será coronado como "El Rey de los Juegos".
Cómo Jugar
Inicio del Juego:
Al iniciar, se presentará una narrativa envolvente que te sumergirá en la historia del juego.
Ingresa tu nombre y selecciona cuántas cartas deseas jugar (entre 1 y 3).
Turnos Estratégicos:
En cada turno, selecciona una carta para moverla usando las flechas del teclado.
Decide si deseas usar la habilidad especial de tu carta para obtener ventajas estratégicas.
Interacción con el Mapa:
Al moverte, puedes caer en trampas que afectarán tus movimientos o recoger cartas mágicas que te ayudarán en tu camino.
Enfréntate a duelistas rivales que intentarán detenerte en tu avance hacia la victoria.
Final del Juego:
El juego termina cuando una carta alcanza la posición del Coliseo. ¡Celebra tu victoria y conviértete en leyenda!
Análisis Detallado del Código
El código del juego se organiza en varias clases que representan diferentes aspectos del juego, como jugadores, cartas, monstruos y el mapa. A continuación se presenta un análisis exhaustivo de las clases y métodos principales, así como su funcionalidad y relación entre sí.
Clases Principales
1. Clase Program
La clase Program es el punto de entrada del juego y contiene la lógica principal que controla el flujo del juego. Aquí se definen los jugadores, el mapa y se manejan las interacciones del juego.
Métodos Principales:
Main():
Este es el método principal que inicia la ejecución del juego. Se encarga de mostrar la historia y las instrucciones, establecer los jugadores y sus cartas, generar el mapa y gestionar el ciclo de juego.
Utiliza un bloque try-catch para manejar excepciones que puedan ocurrir durante la ejecución.
Historia():
Muestra una narrativa introductoria que establece el contexto del juego. Incluye un temporizador que permite a los jugadores leer la historia antes de continuar.
Instrucciones():
Proporciona a los jugadores una guía sobre cómo jugar, incluyendo cómo moverse en el mapa y utilizar las cartas.
Jugar(char[,] mapa):
Contiene la lógica del ciclo de turnos donde cada jugador selecciona una carta para moverla. Verifica si alguna carta ha llegado a la meta y actualiza los estados de las cartas al final de cada turno.
GenerarMapa(int filas, int columnas):
Crea un mapa bidimensional con trampas, duelistas y cartas mágicas colocadas aleatoriamente. La meta se establece en una posición aleatoria del mapa.
ActualizarEnfriamientos():
Actualiza los estados de las cartas al final de cada turno, incluyendo tiempos de enfriamiento y efectos de habilidades.
2. Clase Cartas
La clase Cartas representa las cartas que los jugadores utilizan en el juego. Cada carta tiene un monstruo asociado con atributos específicos y puede realizar acciones como moverse o activar habilidades.
Atributos Principales:
Monstruo: Un objeto de la clase Monstruo, que contiene información sobre el monstruo asociado a la carta.
X, Y: Las coordenadas actuales de la carta en el mapa.
PuedeUsarHabilidad: Un booleano que indica si la carta puede usar su habilidad especial.
SePuedeMover: Indica si la carta puede moverse en el turno actual.
TiempoEnfriamientoRestante: Tiempo restante para poder usar nuevamente la habilidad especial.
SeProtege, TurnosDeProteccion: Controla si la carta está protegida contra ataques o efectos durante un número determinado de turnos.
TurnosStunRestantes, TurnosRestantesReduccionVelocidad: Controlan los efectos de aturdimiento o reducción de velocidad.
Métodos Principales:
Constructor: Inicializa una nueva instancia de Cartas, asignando valores aleatorios para su posición inicial en el mapa y configurando su estado inicial.
Mover(int x, int y): Actualiza las coordenadas de la carta cuando se mueve.
RegresarAPosicionAnterior(): Devuelve la carta a su posición anterior antes de un movimiento (usado en trampas).
ActualizarEnfriamiento(): Disminuye el tiempo restante para el enfriamiento de habilidades.
DisminuirVelocidad(int cantidad): Reduce la velocidad del monstruo asociado a esta carta.
ActivarHabilidad(List<Cartas> cartasEnemigas): Activa la habilidad especial de la carta, afectando a cartas enemigas según lo definido por cada monstruo.
3. Clase Jugador
La clase Jugador representa a cada participante en el juego. Cada jugador tiene un nombre y una lista de cartas que controla durante el juego.
Atributos Principales:
Nombre: El nombre del jugador.
Cartas: Una lista de objetos Cartas, representando las cartas que posee el jugador.
Métodos Principales:
Constructor: Inicializa una nueva instancia de Jugador, asignando un nombre y una lista de cartas.
4. Clase Monstruo
La clase Monstruo define las características específicas de cada monstruo que puede ser representado por una carta.
Atributos Principales:
Nombre: El nombre del monstruo.
Velocidad: La velocidad del monstruo, que determina cuántas posiciones puede moverse en un turno.
TiempoEnfriamiento: Tiempo necesario antes de poder usar nuevamente su habilidad especial.
Habilidad: Descripción textual de la habilidad especial del monstruo.
Métodos Clave en Detalle
Método SeleccionarTurnoJugador(List<Cartas> cartas, char[,] mapa)
Este método permite al jugador seleccionar qué carta mover durante su turno:
Muestra las cartas disponibles junto con sus detalles (nombre, velocidad, tiempo de enfriamiento).
Solicita al jugador que ingrese un número correspondiente a la carta que desea mover.
Verifica si la carta seleccionada puede moverse (no está aturdida).
Si es posible, pregunta si desea usar su habilidad especial antes de proceder a moverla.
Método MoverCarta(Cartas carta, char[,] mapa)
Este método gestiona el movimiento real de una carta en el mapa:
Calcula cuántos movimientos quedan basándose en la velocidad del monstruo.
Permite al jugador elegir una dirección usando las flechas del teclado.
Verifica si el movimiento es válido (no se mueve fuera del mapa o hacia posiciones ocupadas por duelistas o cartas).
Actualiza la posición en el mapa y maneja interacciones con trampas o cartas mágicas encontradas durante el movimiento.
Interacción entre Clases
El diseño modular permite que estas clases interactúen entre sí:
La clase Program, como controlador principal, gestiona instancias de Jugador, creando jugadores y asignándoles instancias de Cartas.
Cada instancia de Cartas, al ser movida por un jugador, interactúa con instancias de otras clases como Monstruo, donde se aplican habilidades especiales o efectos relacionados con trampas.
Dificultades Encontradas
Durante el desarrollo del juego:
Gestión Compleja del Estado del Juego:
Asegurar que todos los estados (como aturdimiento o enfriamiento) se gestionen correctamente fue complicado. Se implementaron métodos específicos para actualizar estos estados al final de cada turno.
Interacción entre Habilidades Especiales:
Las habilidades podían interferir entre sí; por lo tanto, se necesitaba lógica clara para definir cómo interactuaban las habilidades especiales entre diferentes cartas.
Generación Aleatoria del Mapa:
Se requería lógica para evitar colocar múltiples elementos en la misma celda del mapa. Esto se resolvió mediante verificaciones antes de colocar nuevos elementos en posiciones aleatorias.
Interfaz Usuario-Juego:
La interacción mediante consola debe ser clara; se trabajó en mensajes informativos para guiar al usuario durante su experiencia con el juego.
Conclusión
Este juego no solo ofrece una experiencia entretenida llena de estrategia y sorpresas, sino que también es un excelente ejercicio para desarrollar habilidades en programación orientada a objetos con C#. Con su narrativa cautivadora, jugabilidad dinámica y la posibilidad de competir con amigos, ¡este juego promete horas de diversión! Así que reúne a tus amigos, prepara tus cartas y ¡que comience la batalla por ser "El Rey de los Juegos"!
