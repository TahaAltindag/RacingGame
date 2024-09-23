# Racing Demo

This Racing Demo showcases a simple yet engaging racing game that demonstrates realistic vehicle mechanics, sound design, and optimized performance techniques.

## Table of Contents

- [Overview](#overview)
- [Gameplay](#gameplay)
    - [Controls](#controls)
    - [Objective](#objective)
- [Features](#features)
    - [Vehicle Mechanics](#vehicle-mechanics)
    - [User Interface](#user-interface)
    - [Camera System](#camera-system)
    - [Sound Design](#sound-design)
    - [Procedural Generation](#procedural-generation)
    - [Optimization Techniques](#optimization-techniques)
- [Technical Implementation](#technical-implementation)
    - [Design Patterns](#design-patterns)
    - [Event-Driven Architecture](#event-driven-architecture)
    - [Object Pooling](#object-pooling)
    - [GPU Instancing](#gpu-instancing)
- [How to Play](#how-to-play)


## Overview

The demo presents a straightforward yet immersive racing game where the player controls a vehicle that accelerates and decelerates along a straight track. The focus is on realistic acceleration, gear shifting, and engine RPM changes, providing an authentic driving experience. Additional features such as procedural map generation and performance optimizations enhance the overall gameplay.

**Reference Video:** [Vehicle Mechanics Inspiration](https://youtu.be/U-1e7gocYi4?t=117)

## Gameplay

### Controls

- **W Key**: Press and hold to accelerate the vehicle.
- **S Key**: Press and hold to decelerate (brake) the vehicle.

### Objective

- Begin the race after the countdown finishes.
- Accelerate the vehicle to reach the highest possible speed.
- Cross the finish line, approximately 500 meters ahead, in the shortest time.
- Monitor speed, RPM, and gear shifts.

## Features

### Vehicle Mechanics

- **Simple Controls**: Accelerate and brake using the `W` and `S` keys. The vehicle moves straight ahead without steering controls.
- **Automatic Transmission**: The vehicle features an automatic gearbox. As acceleration increases, the engine RPM rises, and the gear shifts up once a certain RPM threshold is reached, causing the RPM to drop.
- **Realistic RPM and Gear Shifts**: Engine RPM and gear shifts are modeled closely to real-world vehicle behavior.

**Gear Max Speeds:**

| Gear | Max Speed (km/h) |
|------|------------------|
| 1    | 52.89            |
| 2    | 89.85            |
| 3    | 127.20           |
| 4    | 174.13           |
| 5    | 241.69           |

### User Interface

- **Speedometer**: Displays the current speed in kilometers per hour (km/h).
- **RPM and Gear Indicator**: Shows the current engine RPM and gear number.
- **Race Timer**: Starts counting after the race begins and stops when the finish line is crossed.
- **Countdown Display**: A 5-second countdown appears before the race starts, ending with a "START!" message.

### Camera System

- **Dynamic Camera**: The camera follows the vehicle and allows 360-degree rotation around it using the mouse.
- **Mouse Control**: Move the mouse to orbit the camera around the vehicle, providing a full view from any angle.

### Sound Design

- **Engine Sound**: Realistic engine sounds are generated using FMOD Studio.
- **RPM-Based Pitch**: The pitch of the engine sound changes dynamically based on the actual RPM value of the vehicle.
- **Custom Audio Events**: Three audio events—`RaceMusic`, `Traffic`, and `Vehicle`—enhance the auditory experience. The `Vehicle` event includes an RPM parameter manipulated by the real RPM value to achieve realistic sound.

### Procedural Generation

- **Map Generation**: The track is procedurally generated, extending ahead as the vehicle progresses.
- **Road and Building Spawning**: Roads and buildings are generated using object pooling to optimize performance.
- **Traffic Simulation**: NPC vehicles are spawned to simulate traffic, adding realism to the environment.

### Optimization Techniques

- **Object Pooling**: Reuses game objects like roads, buildings, and NPC cars to reduce instantiation overhead.
- **GPU Instancing**: Enabled GPU instancing for static meshes to minimize draw calls and improve rendering performance.
- **Event-Driven Design**: Utilized an event-driven architecture to decouple game components, enhancing maintainability and scalability.

## Technical Implementation

### Design Patterns

- **Observer Pattern**: Implemented to allow components to subscribe to events (e.g., speed changes, RPM updates) without tight coupling.
- **Singleton**: Used for the `RaceManager` to ensure a single point of control for the race state.

### Event-Driven Architecture

- **Loose Coupling**: Components communicate through events, promoting a modular and extensible codebase.
- **Scalability**: New features can be added by subscribing to existing events without modifying core logic.

### Object Pooling

- **Performance Optimization**: Reuses objects to reduce garbage collection overhead and instantiation costs.
- **Pooling Manager**: Manages pools of different object types like roads, buildings, and NPC vehicles.

### GPU Instancing

- **Reduced Draw Calls**: Combines multiple instances of the same mesh into a single draw call.
- **Improved Rendering Performance**: Allows for more complex scenes without compromising frame rates.

## How to Play

1. **Start the Game**: Launch the game to begin (If you want to play the game on the editor go to Assets/_Project/Scenes/DemoScene).
2. **Wait for the Countdown**: A 5-second countdown will appear on the screen.
3. **Begin Racing**: Once "START!" is displayed, press and hold the `W` key to accelerate.
4. **Monitor Gauges**: Keep an eye on the speedometer, RPM, and gear indicators to optimize acceleration.
5. **Use Brakes if Needed**: Press the `S` key to decelerate.
6. **Navigate the Track**: The vehicle moves straight ahead; focus on reaching the finish line as quickly as possible.
7. **Finish the Race**: Cross the finish line to end the race and see the final time.


