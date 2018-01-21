# Interfaces Inteligentes: Proyecto Final

## Zombie Labyrinth
|Miembros|Correo|
|-|-|
|Pedro Lagüera Cabrera|alu0100891485@ull.edu.es|
|Abián Torres Torres|alu0100887686@ull.edu.es|

### Introducción
---
Este juego consiste en un simulador de Realidad Virtual basado en Unity y usando la plataforma Cardboard de Google. Se instala como una aplicación en cualquier dispositivo Android y hace uso de los sensores del móvil para seguir la posición y rotación de la cabeza del usuario, el botón magnético del dispositivo Cardboard y de un mando bluetooth conectado al dispositivo.

### Descripción
---
El juego se basa en un sistema de niveles, siendo cada nivel, un laberinto generado al azar de manera procedural. El jugador es emplazado en un lugar al azar dentro del laberinto y también lo son número, que viene determinado por el nivel, de zombies (+Nivel -> + Enemigos) que persiguen al jugador cuando este entra en su campo de visión. El jugador pierde una determinada cantidad de salud cada vez que es atacado por un zombie, pero puede recuperar salud al tocar una poción y atacar a estos y matarlos. Para avanzar de nivel, el jugador necesita buscar y coger una llave que se ha creado en una posición al azar, una vez obtenida la llave el jugador debe buscar el portal que le llevará al siguiente nivel.

### División y Realización del Trabajo

| Tarea | Miembro que la realizó |
|--|--|
| Sistema de Generación de Laberinto | Pedro |
| Diseño de Controles del Jugador | Abián |
| **Unión de Laberinto y Jugador** | Abián / Pedro |
| Disñeo de Sistema de Spawn | Pedro |
| Búsqueda y Preparación de Modelos para Spawn | Abián |
| **Unión de Laberinto y Sistema de Spawn** | Abián / Pedro |
| Búsqueda de Modelos de Jugador y Enemigos | Abián |
| Animator y Animation Scripting de Modelos | Pedro |
| Creación Colliders de Zombies, Jugador y Espada | Pedro |
| Diseño Mecánicas de Ataque y Salud del Jugador y los Enemigos | Pedro |
| Diseño de Game Over y Pause Menu Canvas | Abián |
| **Diseño de Sistema de Eventos de Pausa y Muerte** | Abián / Pedro |
| Implementación de Audio y Efectos de Sonido| Abián |
| **Adaptación del Cámaras a Estéreo de Cardboard** | Abián / Pedro |
| **Adición de Soporte de Gamepad y Botón Magnético** | Abián / Pedro |
| Inclusión de Pantalla de Inicio | Pedro |
