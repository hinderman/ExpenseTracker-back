# ExpenseTracker - Backend

Sistema API para la gestión y seguimiento de solicitudes de gastos. Aplicación desarrollada con arquitectura limpia siguiendo principios de Domain-Driven Design (DDD), implementando patrones modernos de desarrollo en .NET.

## 📋 Tabla de Contenidos

- [Descripción General](#descripción-general)
- [Requisitos Previos](#requisitos-previos)
- [Instalación y Configuración](#instalación-y-configuración)
- [Cómo Ejecutar el Proyecto](#cómo-ejecutar-el-proyecto)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Arquitectura](#arquitectura)
- [Patrones de Diseño](#patrones-de-diseño)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Estilo de Codificación](#estilo-de-codificación)
- [API Endpoints](#api-endpoints)
- [Configuración de Base de Datos](#configuración-de-base-de-datos)
- [Contribución](#contribución)

---

## 📄 Descripción General

**ExpenseTracker** es una API REST desarrollada en .NET 10.0 que permite la gestión integral de solicitudes de gastos. El sistema permite:

- **Crear** nuevas solicitudes de gastos
- **Consultar** solicitudes con filtros avanzados (estado, categoría, rango de fechas)
- **Actualizar** información de solicitudes existentes
- **Eliminar** solicitudes
- **Paginar** resultados de consultas
- **Gestionar** usuarios, categorías, monedas y estados

El proyecto está diseñado siguiendo principios de arquitectura limpia, asegurando:
- Separación de responsabilidades
- Facilidad de testeo
- Mantenibilidad a largo plazo
- Escalabilidad

---

## 📦 Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- **[.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)** o superior
- **[Visual Studio 2022](https://visualstudio.microsoft.com/)** (Recomendado) o **VS Code** con extensiones C#
- **[PostgreSQL 14+](https://www.postgresql.org/download/)** - Base de datos
- **[Git](https://git-scm.com/)**

### Verificar la instalación

```bash
dotnet --version
dotnet --list-sdks
```

---

## 🔧 Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/hinderman/ExpenseTracker-back.git
cd ExpenseTracker-back
```

### 2. Restaurar Dependencias

```bash
dotnet restore
```

### 3. Configurar la Base de Datos

Edita el archivo `Api/appsettings.json` y configura la cadena de conexión de PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ExpenseTrackerDb;Username=postgres;Password=tu_contraseña"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## ▶️ Cómo Ejecutar el Proyecto

### Opción 1: Desde Visual Studio

1. Abre `ExpenseTracker.slnx` en Visual Studio 2022
2. Asegúrate que **Api** esté seleccionado como proyecto de inicio
3. Presiona `F5` o haz clic en **Start** (▶)
4. La API se abrirá en `https://localhost:7001` (o el puerto configurado)

### Opción 2: Desde la Terminal

```bash
# En la raíz del proyecto
dotnet run --project Api/Api.csproj

# O accede al directorio Api y ejecuta
cd Api
dotnet run
```

---

## 📁 Estructura del Proyecto

```
ExpenseTracker/
│
├── Api/                           # Presentación - Controladores y configuración HTTP
│   ├── Controllers/
│   │   └── ExpenseRequestController.cs   # Endpoints de gastos
│   ├── Common/
│   │   └── Errors/
│   │       └── ErrorExtensions.cs        # Extensiones de manejo de errores
│   ├── Contracts/
│   │   └── ExpenseRequest/
│   │       ├── CreateRequestDto.cs       # DTO para crear solicitud
│   │       └── UpdateRequestDto.cs       # DTO para actualizar solicitud
│   ├── Properties/
│   │   └── launchSettings.json           # Configuración de ejecución
│   ├── Program.cs                        # Punto de entrada y configuración
│   ├── appsettings.json                  # Configuración de la aplicación
│   └── Api.csproj
│
├── Application/                   # Lógica de aplicación - Casos de uso
│   ├── ExpenseRequest/
│   │   ├── Commands/
│   │   │   ├── Create.cs                 # Comando para crear gasto
│   │   │   ├── Update.cs                 # Comando para actualizar gasto
│   │   │   ├── Delete.cs                 # Comando para eliminar gasto
│   │   │   └── Handlers/
│   │   │       ├── CreateHandler.cs
│   │   │       ├── UpdateHandler.cs
│   │   │       └── DeleteHandler.cs
│   │   ├── Queries/
│   │   │   ├── GetAll.cs                 # Query para obtener todos los gastos
│   │   │   ├── GetById.cs                # Query para obtener gasto por ID
│   │   │   └── Handlers/
│   │   │       ├── GetAllHandler.cs
│   │   │       └── GetByIdHandler.cs
│   │   └── Dtos/
│   │       ├── DetailDto.cs              # DTO con detalles completos
│   │       └── SummaryDto.cs             # DTO resumido
│   ├── Interfaces/
│   │   └── Queries/
│   │       └── IExpenseRequestQueries.cs  # Interfaz de queries
│   ├── Common/
│   │   └── Models/
│   │       └── Pagination.cs             # Modelo de paginación genérica
│   └── Application.csproj
│
├── Domain/                        # Lógica de negocio - Entidades y reglas
│   ├── Aggregates/
│   │   └── ExpenseRequest.cs             # Agregado principal (raíz agregada)
│   ├── Entities/
│   │   ├── User.cs                       # Entidad Usuario
│   │   ├── Category.cs                   # Entidad Categoría
│   │   ├── Status.cs                     # Entidad Estado
│   │   └── Currency.cs                   # Entidad Moneda
│   ├── ValueObjects/
│   │   ├── Amount.cs                     # Value Object para montos
│   │   └── Email.cs                      # Value Object para emails
│   ├── Common/
│   │   ├── BaseEntity.cs                 # Clase base para entidades
│   │   ├── AuditableEntity.cs            # Clase con auditoría (created, updated)
│   ├── Interfaces/
│   │   ├── IExpenseRequestRepository.cs   # Interfaz del repositorio
│   │   └── IUnitOfWork.cs                # Interfaz Unit of Work
│   ├── Exceptions/
│   │   └── DomainException.cs            # Excepciones del dominio
│   └── Domain.csproj
│
├── Infrastructurr/                # Implementaciones técnicas
│   ├── Persistence/
│   │   ├── DatabaseContext.cs            # DbContext de Entity Framework
│   │   ├── Configurations/               # Mapeos de entidades a tablas
│   │   └── Repositories/
│   │       ├── ExpenseRequestRepository.cs # Implementación del repositorio
│   │       └── ExpenseRequestQueries.cs    # Implementación de queries
│   ├── DependencyInjection.cs            # Inyección de dependencias
│   └── Infrastructure.csproj
│
├── ExpenseTracker.slnx            # Solución de Visual Studio
├── README.md                      # Archivo de explicacion (Estamos aqui)
└── .gitignore                     # Archivo para ignorar en Git
```

---

## 🏛️ Arquitectura

El proyecto está basado en **Arquitectura Limpia (Clean Architecture)** combinada con principios de **Domain-Driven Design (DDD)**. Esta arquitectura está organizada en 4 capas principales:

### 1. **Capa Api (Presentación)**
- **Responsabilidad**: Exponer endpoints HTTP y recibir/enviar datos del cliente
- **Componentes**: Controladores, DTOs, Filtros, Middleware
- **Dependencias**: Apunta hacia Application
- **Características**:
  - Controladores API REST
  - Validación de entrada
  - Manejo de códigos HTTP
  - CORS y configuración de seguridad

### 2. **Capa Application (Casos de Uso)**
- **Responsabilidad**: Orquestar la lógica de operaciones del negocio
- **Componentes**: Commands, Queries, Handlers, DTOs, Servicios
- **Patrón**: CQRS (Command Query Responsibility Segregation)
- **Dependencias**: Apunta hacia Domain
- **Características**:
  - Casos de uso independientes de la tecnología
  - Mapeo entre entidades de dominio y DTOs
  - Orquestación de múltiples repositorios

### 3. **Capa Domain (Lógica de Negocio)**
- **Responsabilidad**: Contener la lógica de negocio pura e independiente
- **Componentes**: Entidades, Agregados, Value Objects, Excepciones, Interfaces
- **Patrón**: Domain-Driven Design
- **Características**:
  - No tiene dependencias exactas (solo interfaces)
  - Define las reglas del negocio
  - Encapsulamiento de invariantes
  - Value Objects inmutables

### 4. **Capa Infrastructure (Implementación Técnica)**
- **Responsabilidad**: Implementar la persistencia, acceso a datos y servicios externos
- **Componentes**: DbContext, Repositorios, UnitOfWork, Inyección de Dependencias
- **Dependencias**: Apunta hacia Domain y Application
- **Características**:
  - Entity Framework Core con PostgreSQL
  - Implementación de patrones de acceso a datos
  - Configuración de base de datos

### Flujo de Datos

```
HTTP Request
    ↓
Controller (Api)
    ↓
IRequest (Application)
    ↓
Handler (Application)
    ↓
Domain (Lógica de Negocio)
    ↓
Repository (Infrastructure)
    ↓
Database (PostgreSQL)
    ↓
Response
```

---

## 🎨 Patrones de Diseño

### 1. **CQRS (Command Query Responsibility Segregation)**
Separa las operaciones de lectura (Queries) de de escritura (Commands).

**Beneficios**:
- Optimizar lecturas y escrituras independientemente
- Escalabilidad mejorada
- Lógica más clara y separada

### 2. **Repository Pattern**
Abstrae la lógica de acceso a datos.

**Beneficios**:
- Código testeable
- Cambiar implementación sin afectar otras capas
- Abstracción de detalles de persistencia

### 3. **Unit of Work Pattern**
Coordina múltiples repositorios y transacciones.

**Beneficios**:
- Transacciones consistentes
- Control centralizado de cambios

### 4. **Aggregate Pattern (DDD)**
Agrupa entidades y value objects relacionados bajo una raíz agregada.

**Beneficios**:
- Modela límites transaccionales
- Asegura invariantes de negocio
- Cohesión de datos relacionados

### 5. **Value Object Pattern**
Objetos inmutables que representan conceptos del dominio.

**Beneficios**:
- Validación en el dominio
- Semántica clara
- Inmutabilidad

### 6. **Factory Pattern**
Crea objetos de dominio manteniendo reglas de negocio.

### 7. **Dependency Injection**
Inversión de dependencias a través de inyección.

### 8. **Handler Pattern (MediatR)**
Usa MediatR para desacoplar comandos/queries de sus manipuladores.

---

## 💻 Tecnologías Utilizadas

| Categoría | Tecnología | Versión | Descripción |
|-----------|-----------|---------|------------|
| **Framework** | .NET | 10.0 | Framework oficial de Microsoft |
| **Web API** | ASP.NET Core | 10.0 | Framework para APIs REST |
| **Base de Datos** | PostgreSQL | 14+ | Sistema de gestión de BD relacional |
| **ORM** | Entity Framework Core | Última | Mapeo objeto-relacional |
| **Driver BD** | Npgsql | 10.0.1 | Driver PostgreSQL para .NET |
| **CQRS/Mediador** | MediatR | 14.1.0 | Implementación de patrón mediador |
| **Manejo de Errores** | ErrorOr | 2.0.1 | Management de resultados y errores |
| **Lenguaje** | C# | 13 | Lenguaje de programación |

---

## 📝 Estilo de Codificación

El proyecto sigue convenciones y estándares consistentes para asegurar legibilidad y mantenibilidad:

### 1. **Convención de Nombres de Parámetros**

Todos los parámetros usan el prefijo `prm` (parámetro):

```csharp
public async Task Handle(Create prmRequest, CancellationToken prmCancellationToken)
{
    // prmRequest, prmCancellationToken, etc.
}
```

**Ventajas**:
- Claramente distingue parámetros de variables locales
- Evita conflictos de nombres
- Mejora la legibilidad

### 2. **Sealed Classes**

Las clases se marcan como `sealed` cuando no están diseñadas para ser heredadas:

```csharp
public sealed class ExpenseRequest : AuditableEntity
{
    // Clase sellada - no puede heredarse
}
```

**Ventajas**:
- Explícita intención de diseño
- Mejor rendimiento (sin búsqueda virtual)
- Previene herencia no intencional

### 3. **Registros (Records) para Inmutabilidad**

Commands y Queries se definen como records:

```csharp
public sealed record Create(
    Guid RequestedById, 
    Guid CategoryId, 
    Guid StatusId, 
    Guid CurrencyId, 
    decimal Amount, 
    DateTime ExpenseDate, 
    string? Description = null) 
    : IRequest<ErrorOr<Unit>>;
```

**Ventajas**:
- Inmutabilidad garantizada
- Sintaxis concisa
- Métodos `ToString()`, `Equals()` auto-generados

### 4. **Null Safety**

El proyecto habilita `<Nullable>enable</Nullable>`:

```csharp
// Tipos no-nullable por defecto
public Guid Id { get; private set; }

// Tipos nullable se marcan explícitamente con '?'
public string? Description { get; private set; }
```

**Ventajas**:
- Previene excepciones de referencia nula
- Código más seguro
- Compilador ayuda a detectar errores

### 5. **Encapsulamiento de Propiedades**

Las propiedades son `private set` para mantener encapsulamiento:

```csharp
public Guid RequestedById { get; private set; }  // Solo lectura desde afuera
```

### 6. **Métodos Factory com Static**

Creación de objetos mediante métodos estáticos:

```csharp
public static ExpenseRequest Create(...)
{
    // Lógica de validación y creación
    return new ExpenseRequest(...);
}
```

### 7. **Inyección de Dependencias en Constructores**

Las dependencias se inyectan via parámetros primarios (primary constructor):

```csharp
public sealed class CreateHandler(
    IExpenseRequestRepository prmRepository,
    IUnitOfWork prmUnitOfWork)
    : IRequestHandler<Create, ErrorOr<Unit>>
{
    // Acceso directo a prmRepository y prmUnitOfWork
}
```

### 8. **Manejo de Errores com ErrorOr**

Se retorna `ErrorOr<T>` en lugar de excepciones:

```csharp
ErrorOr<Unit> objResult = await prmISender.Send(command);

return objResult.Match(
    success => Created(),
    error => ErrorExtensions.Problem(error)
);
```

### 9. **Comentarios y Documentación**

Se usa XML documentation para métodos públicos:

```csharp
/// <summary>
/// Obtiene un gasto por su identificador único.
/// </summary>
/// <param name="prmId">ID del gasto a recuperar</param>
public async Task<IActionResult> GetById(Guid prmId)
{
    // Implementación
}
```

### 10. **Formato de Código**

- **Indentación**: 4 espacios
- **Línea máxima**: 120 caracteres
- **Orden de miembros**: Propiedades, Métodos, Eventos
- **Visibilidad** Privada → Interna → Pública

---

## 🔌 API Endpoints

### ExpenseRequest Controller

#### **Obtener todas las solicitudes**

```http
GET /api/expenserequest?statusId=guid&categoryId=guid&startDate=date&endDate=date&pageNumber=1&pageSize=10
```

**Parámetros de Query**:
- `statusId` (opcional): Filtrar por estado
- `categoryId` (opcional): Filtrar por categoría
- `startDate` (opcional): Fecha de inicio
- `endDate` (opcional): Fecha de fin
- `pageNumber` (opcional, default: 1): Número de página
- `pageSize` (opcional, default: 10): Tamaño de página

**Respuesta de Éxito** (200 OK):
```json
{
  "totalCount": 50,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 5,
  "items": [
    {
      "id": "guid",
      "amount": 1500.00,
      "description": "Gasto de transporte",
      "expenseDate": "2026-03-31",
      "categoryId": "guid",
      "statusId": "guid"
    }
  ]
}
```

---

#### **Obtener solicitud por ID**

```http
GET /api/expenserequest/{id}
```

**Parámetros de Ruta**:
- `id` (guid): ID de la solicitud

**Respuesta de Éxito** (200 OK):
```json
{
  "id": "guid",
  "requestedById": "guid",
  "categoryId": "guid",
  "statusId": "guid",
  "currencyId": "guid",
  "amount": 1500.00,
  "description": "Detalle completo del gasto",
  "expenseDate": "2026-03-31",
  "createdAt": "2026-03-31T10:30:00Z",
  "updatedAt": "2026-03-31T10:30:00Z"
}
```

---

#### **Crear nueva solicitud**

```http
POST /api/expenserequest
Content-Type: application/json

{
  "requestedById": "guid",
  "categoryId": "guid",
  "statusId": "guid",
  "currencyId": "guid",
  "amount": 1500.00,
  "expenseDate": "2026-03-31",
  "description": "Descripción del gasto"
}
```

**Respuesta de Éxito** (201 Created):
```json
{}
```

---

#### **Actualizar solicitud**

```http
PUT /api/expenserequest/{id}
Content-Type: application/json

{
  "categoryId": "guid",
  "statusId": "guid",
  "currencyId": "guid",
  "amount": 2000.00,
  "expenseDate": "2026-04-01",
  "description": "Descripción actualizada"
}
```

**Respuesta de Éxito** (204 No Content):
```
(sin cuerpo)
```

---

#### **Aprobar solicitud**

```http
PATCH /api/expenserequest/{id}/approve
```

**Respuesta de Éxito** (204 No Content):
```
(sin cuerpo)
```

---

#### **Rechazar solicitud**

```http
PATCH /api/expenserequest/{id}/reject
```

**Respuesta de Éxito** (204 No Content):
```
(sin cuerpo)
```

---

#### **Eliminar solicitud**

```http
DELETE /api/expenserequest/{id}
```

**Respuesta de Éxito** (204 No Content):
```
(sin cuerpo)
```

---

## 🗄️ Configuración de Base de Datos

### Variables de Conexión

Actualiza `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ExpenseTrackerDb;Username=postgres;Password=tu_contraseña"
  }
}
```

---

## 🤝 Contribución

Por favor, sigue estos pasos para contribuir:

1. **Fork** el repositorio
2. **Crea una rama** para tu funcionalidad (`git checkout -b feature/AmazingFeature`)
3. **Commits** con mensajes claros (`git commit -m 'Add AmazingFeature'`)
4. **Push** a la rama (`git push origin feature/AmazingFeature`)
5. **Abre un Pull Request**

### Guía de Commits

- `feat:` Nueva funcionalidad
- `fix:` Corrección de bug
- `docs:` Cambios en documentación
- `refactor:` Refactorización de código
- `test:` Adición de tests
- `chore:` Cambios en build/dependencias

---

## 👨‍💻 Autor

Desarrollado por **Hinderman (PhD_Nobody)**