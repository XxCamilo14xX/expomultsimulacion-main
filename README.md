# 🎮 Simulación Multijuegos - Proyecto Unity

## 📋 Descripción General

Este proyecto es una aplicación interactiva de Unity que simula un entorno de escritorio virtual con múltiples minijuegos y experiencias integradas. El usuario puede navegar por un cuarto virtual, interactuar con diferentes objetos y acceder a diversas simulaciones y juegos.

## 🌟 Características Principales

### 🖥️ Escritorio Virtual (Windowsescritorio)
- Simulación de un escritorio de Windows
- Interfaz interactiva con iconos clickeables
- Acceso a diferentes aplicaciones y juegos

### 🏠 Cuarto Principal
- Entorno 3D navegable en primera persona
- Interacción con objetos del cuarto
- Acceso a la PC que abre el escritorio virtual

### ✈️ Simulador de Vuelo (Solar System)
Un simulador de vuelo espacial completo con:
- **Física realista de vuelo**
  - Sistema de sustentación y resistencia aerodinámica
  - Control de aceleración (throttle)
  - Flaps para aterrizaje
  - Sistema de G-forces
  
- **Armamento**
  - Cañones con munición ilimitada
  - Misiles con sistema de bloqueo
  - HUD con información de objetivos
  
- **Controles duales** (Teclado/Mouse + Xbox Controller)
  - **Movimiento**: Stick izquierdo / WASD
  - **Acelerador**: RB/LB / Shift/Ctrl
  - **Yaw (guiñada)**: RT/LT / Q/E
  - **Cámara**: Stick derecho / Mouse
  - **Flaps**: D-pad abajo / F
  - **Disparar misil**: B / Clic derecho
  - **Disparar cañón**: A / Clic izquierdo
  - **Reiniciar** (al morir): Y / R
  - **Volver al escritorio** (al morir): Start / Escape

- **Sistema de muerte y respawn**
  - Efectos visuales de daño
  - Pantalla de Game Over
  - Opciones para reiniciar o volver al menú

### 🚀 Nave Espacial
Simulación de una nave espacial con:
- **Física orbital realista**
  - Gravedad de N-cuerpos
  - Propulsores omnidireccionales
  - Rotación libre en 6 grados de libertad
  
- **Sistema de navegación**
  - HUD con información de planetas
  - Sistema de bloqueo de objetivos celestes
  - Indicadores de velocidad relativa
  - Información de distancia a planetas
  
- **Controles completos** (Teclado/Mouse + Xbox Controller)
  - **Movimiento horizontal/adelante**: Stick izquierdo / WASD
  - **Subir/Bajar**: RB/LB / Espacio/Shift
  - **Rotar cámara**: Stick derecho / Mouse
  - **Roll (rotar nave)**: RT/LT / E/Q
  - **Seleccionar planeta**: A / Clic izquierdo
  - **Abrir/Cerrar escotilla**: D-pad arriba / G
  - **Salir del asiento**: X / F
  - **Volver al escritorio**: Start / Escape

- **Interacción con el piloto**
  - Entrar y salir de la nave
  - Animación de escotilla
  - Transición suave entre controles de avión y nave

### 🐦 Flappy Bird
Clon del clásico juego con:
- Física de gravedad y salto
- Sistema de puntuación
- Generación procedural de obstáculos
- Pantalla de Game Over con reinicio
- Soporte para teclado y gamepad

### 🐜 Simulador de Colonia de Hormigas
Simulación compleja de comportamiento de hormigas:
- **Tipos de hormigas**
  - Reina: Reproduce y crea nuevas hormigas
  - Trabajadoras: Recolectan comida
  - Excavadoras: Construyen túneles
  
- **Sistema de feromonas**
  - Feromonas de comida
  - Feromonas de hogar
  - Difusión y evaporación realista
  
- **Comportamiento emergente**
  - Búsqueda de comida
  - Construcción de caminos
  - Formación de colonias
  
- **Integración con Tilemap**
  - Terreno editable
  - Obstáculos
  - Zonas de comida

### 🎨 Paint
Aplicación de dibujo simple:
- Herramientas de pintura
- Selección de colores
- Borrador
- Guardar y cargar dibujos

## 🎯 Sistema de Input Unificado

El proyecto utiliza el **Unity Input System** para proporcionar soporte completo tanto para teclado/mouse como para mandos Xbox. Todos los controles están mapeados en el archivo `PlayerInput.inputactions` con dos mapas principales:

### Mapa "Plane" (Avión)
- Controles de vuelo
- Armamento
- Opciones de Game Over

### Mapa "Spaceship" (Nave Espacial)
- Controles de propulsión
- Navegación orbital
- Interacción con la nave
- Selección de planetas

## 🏗️ Arquitectura del Proyecto

### Scripts Principales

#### `PlanePlayerController.cs`
- Maneja la entrada del jugador para el avión
- Gestiona la cámara y el HUD
- Controla el sistema de IA opcional
- Maneja las transiciones de escena

#### `Plane.cs`
- Física de vuelo realista
- Sistema de daño y salud
- Armamento (misiles y cañones)
- Animaciones y efectos visuales

#### `Ship.cs`
- Física orbital y propulsión
- Sistema de gravedad de N-cuerpos
- Interacción con el piloto
- Controles de escotilla

#### `ShipHUD.cs`
- Interfaz de usuario de la nave
- Sistema de bloqueo de planetas
- Indicadores de velocidad relativa
- Información de navegación

### Escenas

1. **Windowsescritorio.unity** - Escritorio virtual principal
2. **cuarto principal.unity** - Cuarto en primera persona
3. **Solar System.unity** - Simulador de vuelo espacial
4. **flapibird.unity** - Juego Flappy Bird
5. **hormiguero.unity** - Simulador de hormigas
6. **Paint.unity** - Aplicación de dibujo

## 🎮 Controles Completos

### Avión (Solar System)
| Acción | Teclado/Mouse | Xbox Controller |
|--------|---------------|-----------------|
| Pitch/Roll | W/A/S/D | Stick Izquierdo |
| Yaw | Q/E | LT/RT |
| Acelerador | Shift/Ctrl | RB/LB |
| Cámara | Mouse | Stick Derecho |
| Flaps | F | D-pad Abajo |
| Disparar Cañón | Clic Izquierdo | A |
| Disparar Misil | Clic Derecho | B |
| Reiniciar (muerto) | R | Y |
| Salir (muerto) | Escape | Start |

### Nave Espacial
| Acción | Teclado/Mouse | Xbox Controller |
|--------|---------------|-----------------|
| Movimiento | W/A/S/D | Stick Izquierdo |
| Vertical | Espacio/Shift | RB/LB |
| Rotar Cámara | Mouse | Stick Derecho |
| Roll | Q/E | LT/RT |
| Seleccionar Planeta | Clic Izquierdo | A |
| Escotilla | G | D-pad Arriba |
| Salir del Asiento | F | X |
| Volver al Escritorio | Escape | Start |

### Cuarto/Escritorio
| Acción | Teclado/Mouse | Xbox Controller |
|--------|---------------|-----------------|
| Movimiento | W/A/S/D | Stick Izquierdo |
| Mirar | Mouse | Stick Derecho |
| Interactuar | E | A |
| Saltar | Espacio | A |

## 🔧 Requisitos Técnicos

- **Unity Version**: 2021.3 o superior
- **Input System Package**: Instalado y configurado
- **TextMesh Pro**: Para UI avanzada
- **Paquetes adicionales**:
  - Unity Input System
  - TextMesh Pro
  - Cinemachine (opcional para cámaras)

## 📦 Instalación y Uso

1. **Clonar el repositorio**
   ```bash
   git clone [URL_DEL_REPOSITORIO]
   ```

2. **Abrir en Unity**
   - Abrir Unity Hub
   - Seleccionar "Add" y navegar a la carpeta del proyecto
   - Abrir con Unity 2021.3 o superior

3. **Configurar Input System**
   - El proyecto ya incluye `PlayerInput.inputactions`
   - Asegurarse de que esté asignado en los GameObjects correspondientes

4. **Ejecutar**
   - Abrir la escena `Windowsescritorio.unity`
   - Presionar Play

## 🎨 Características Visuales

- **Efectos de partículas**
  - Humo del avión
  - Explosiones
  - Efectos de propulsores
  
- **Post-procesamiento**
  - Bloom
  - Motion blur en vuelo
  - Color grading

- **Iluminación**
  - Sistema de iluminación global
  - Sombras en tiempo real
  - Iluminación dinámica para planetas

## 🐛 Solución de Problemas

### Los controles del gamepad no funcionan
1. Verificar que el `PlayerInput` component tenga asignado `PlayerInput.inputactions`
2. Asegurarse de que **Behavior** esté en "Invoke C Sharp Events"
3. Verificar que el **Default Map** sea correcto ("Plane" o "Spaceship")

### El avión/nave no responde
1. Verificar que el GameObject tenga el componente `PlayerInput`
2. Revisar la consola para errores de referencia nula
3. Asegurarse de que el action map correcto esté activo

### Error "Cannot find action map"
1. Cerrar Unity completamente
2. Volver a abrir el proyecto
3. Seleccionar `PlayerInput.inputactions` y hacer clic derecho → Reimport

## 👥 Créditos

Proyecto desarrollado como demostración de múltiples sistemas de Unity integrados en una sola aplicación.

## 📝 Notas de Desarrollo

- El proyecto utiliza el nuevo Input System de Unity para máxima compatibilidad
- Todos los scripts están comentados para facilitar el mantenimiento
- La arquitectura está diseñada para ser modular y extensible
- Se incluyen sistemas de física realistas para vuelo y órbitas

## 🚀 Futuras Mejoras

- [ ] Más minijuegos en el escritorio
- [ ] Sistema de guardado de progreso
- [ ] Multijugador en red
- [ ] Más planetas y cuerpos celestes
- [ ] Editor de niveles para Flappy Bird
- [ ] Mejoras en la IA de las hormigas
- [ ] Soporte para más tipos de controles (joystick, volante, etc.)

---

**Versión**: 1.0  
**Última actualización**: Noviembre 2025
