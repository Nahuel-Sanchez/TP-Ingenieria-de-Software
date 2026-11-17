# Horizon Hotel & Resort — Sistema de gestión hotelera

Aplicación de escritorio en C# WinForms (.NET Framework 4.8) con base de datos SQL Server (LocalDB).

**Esta entrega es el PN2, Cargos adicionales.** Abarca:
- CUN-06 Renovar Estadía
- CUN-07 Registrar Pedido de Servicio a la Habitación
- CUN-08 Registrar Trabajo Finalizado
- CUN-09 Registrar Cambio de Habitación

También incluye la bitácora de cambios de reservas, con historial y rollback.

---

## 1. Puesta en marcha

### 1.1 Requisitos

- Windows 10 u 11.
- **Visual Studio 2022 (17.13 o posterior) o Visual Studio 2026**, con el componente **SQL Server Express LocalDB**.
  - Viene en la carga de trabajo «Almacenamiento y procesamiento de datos», o se puede agregar desde el Visual Studio Installer.
  - La solución está en formato `.slnx`, que versiones anteriores no abren.
- .NET Framework 4.8.
- Para cargar la base con datos: **SQL Server Management Studio (SSMS)** o `sqlcmd`.

### 1.2 Pasos

**1. Crear la base con los datos de prueba (antes de abrir la aplicación).**

El archivo `script.sql` de la raíz del repositorio crea la base `TP_Ing_Soft` completa:
- estructura;
- procedimientos y triggers;
- el usuario de la sección 2;
- habitaciones, huéspedes y reservas de ejemplo;
- dígitos verificadores.

Para ejecutarlo hay dos opciones:

- **Con SSMS:** conectarse a `(localdb)\MSSQLLocalDB` con autenticación de Windows, abrir `script.sql` y ejecutarlo completo (F5).
- **Con `sqlcmd`**, desde la carpeta del repositorio:

  ```bat
  sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i script.sql
  ```

> Si la base ya existe, el script no hace nada y lo avisa. Nunca pisa datos.

**2. Abrir la solución.** Abrir `TP Ingenieria de Software.slnx` con Visual Studio.

**3. Agregar el paquete NuGet FontAwesome.Sharp.** La interfaz lo usa para los íconos y sin él la solución no compila.
1. Menú **Herramientas › Administrador de paquetes NuGet › Administrar paquetes NuGet para la solución…**
2. Pestaña **Examinar**: buscar **FontAwesome.Sharp**.
3. Marcar el proyecto **GUI_08YS**, elegir la versión **6.6.0** e **Instalar**.

También se puede hacer desde la consola del Administrador de paquetes:

```powershell
Install-Package FontAwesome.Sharp -Version 6.6.0 -ProjectName GUI_08YS
```

El otro paquete, Newtonsoft.Json, se restaura solo al compilar. Para eso hace falta conexión a internet.

**4. Ejecutar.**
1. Definir **GUI_08YS** como proyecto de inicio.
2. Compilar y ejecutar (F5).

La cadena de conexión ya apunta a `(localdb)\MSSQLLocalDB`. Si la instancia de LocalDB no existe o está detenida, la aplicación la crea y la inicia sola.

> **Importante:** si se abre la aplicación **antes** de correr `script.sql`, la aplicación crea sola una base nueva sin estos datos ni el usuario de la sección 2. En ese caso:
> 1. Borrar la base, desde SSMS o con el comando de abajo.
> 2. Correr `script.sql`.
>
> ```bat
> sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "DROP DATABASE TP_Ing_Soft"
> ```

---

## 2. Usuario para probar

| Usuario | Contraseña | Rol |
|---|---|---|
| `Nahu` | `123` | SuperAdmin: tiene acceso a todo el sistema, incluido lo del recepcionista y lo del personal de servicio. |

**Datos útiles:**

- **Huésped de prueba:** documento `33088321`, Emiliano Acosta.
  - También se puede registrar un huésped nuevo desde la misma pantalla de la reserva.
- **Pago con tarjeta:** `4111 1111 1111 1111`, con cualquier vencimiento futuro y CVV de 3 dígitos.
  - También se puede pagar en efectivo.
- **Ayuda:** F1 muestra la ayuda de la pantalla actual. Shift+F1 explica el control que está debajo del puntero.

---

## 3. Recorrido del PN2 (entrega actual)

Los cuatro casos de uso trabajan sobre una **estadía en curso**, es decir, una reserva con check-in hecho. Por eso el recorrido empieza preparando una.

### 3.0 Preparación: reserva y check-in de hoy

1. **Recepción › Reservar**:
   - Elegir **hoy** como llegada y una salida a **3 o 4 noches**, con **1 adulto**.
   - Clic en **Elegir habitación** y elegir una habitación libre.
2. En **Nueva reserva**:
   - Buscar al titular con el documento `33088321` (**Consultar**).
   - Elegir el método de pago y **Confirmar reserva**.
3. **Recepción › Check-In**:
   - **Buscar Reserva por DNI** (`33088321`).
   - **Confirmar Check-in**.

   El check-in solo se puede hacer el día de ingreso, por eso la reserva tiene que empezar hoy.

Desde acá, todo se hace desde **Recepción › Control de habitaciones**. La habitación del huésped aparece **Ocupada**; al hacer clic sobre ella se abre el menú de acciones de la estadía.

### 3.1 CUN-06 Renovar Estadía

1. En la habitación ocupada, elegir **Renovar estadía**.
2. Elegir la **Nueva Fecha de Egreso**. Se ven las noches adicionales y su costo.
3. **Confirmar**.

**Qué controlar:**
- La fecha de egreso de la reserva cambia.
- El costo adicional queda como saldo a cobrar en el check-out. Se ve en **Ver detalle de la estadía**.
- Si la habitación ya está reservada por otra persona en esas noches, el sistema no permite renovar.

### 3.2 CUN-07 Registrar Pedido de Servicio a la Habitación

1. En la habitación ocupada, elegir **Servicio a la habitación**.
   - También se llega desde **Room service › Nuevo pedido**.
2. Doble clic sobre los ítems de la carta para sumarlos al pedido. Las cantidades se ajustan con + y −.
3. **Registrar pedido**.

**Qué controlar:**
- El pedido se carga a la cuenta de la habitación.
- El pedido aparece como pendiente para el personal de servicio, que es el paso siguiente.

### 3.3 CUN-08 Registrar Trabajo Finalizado

Este caso lo hace el **Personal de servicio**.

1. **Room service › Pedidos de room service**, en el filtro **Pendientes**.
2. En el pedido del paso anterior, hacer clic en el botón **Confirmar entrega** (✓) y aceptar.

**Qué controlar:**
- El pedido pasa a **Entregados hoy** con la hora de entrega y el tiempo de espera.
- Los filtros permiten buscar por habitación, huésped y monto.

### 3.4 CUN-09 Registrar Cambio de Habitación

1. En la habitación ocupada, elegir **Cambio de habitación**.
   - También se llega desde **Recepción › Cambio de habitación**.
2. Elegir el motivo:
   - **Pedido por el huésped**: solo a una habitación de **tarifa mayor**, libre hasta el egreso, y con al menos una noche por delante. Se cobra la diferencia de tarifa por las noches que quedan.
   - **Reubicación por el hotel**: a cualquier habitación libre hasta el egreso, **sin cargo**. El huésped mantiene su tarifa.
3. Elegir la habitación nueva, revisar el cargo calculado y **Confirmar cambio**.

**Qué controlar:**
- La reserva queda en la habitación nueva.
- La habitación anterior pasa a limpieza.
- En el cambio pedido por el huésped, el cargo aparece en la cuenta.

> La reubicación también se dispara sola si se elige **Poner en Mantenimiento** sobre una habitación ocupada. Primero hay que mudar al huésped y recién después la habitación queda fuera de servicio.

### 3.5 Cierre: ver todos los cargos

- En la habitación, **Ver detalle de la estadía** muestra los movimientos de la cuenta:
  - alojamiento;
  - renovación;
  - pedidos;
  - cambio de habitación;
  - saldo pendiente.
- **Recepción › Check-Out** cobra el saldo, incluidos todos los cargos adicionales.

### 3.6 Bitácora de reservas

**Análisis › Bitácora de reservas**:
- Cada alta o cambio de una reserva deja un registro con la foto completa de la reserva, la operación, la fecha, la hora y el usuario.
- Para el recorrido anterior aparecen: Alta, Check-in, Renovación, Cambio de habitación por huésped o administrativo, y Check-out.
- Al elegir un registro se ve, campo por campo, qué cambió respecto del anterior.
- **Ver historial** muestra todos los registros de la reserva.
- **Restaurar como activo** hace un rollback: vuelve la reserva a un registro anterior.

### 3.7 Análisis

- **Dashboard**: tiene indicadores de room service y cambios de habitación del período.
- **Reportes › Reporte de servicios**: muestra los pedidos y cargos adicionales.

---

## 4. Cambiar la cadena de conexión

Solo hace falta si no se usa LocalDB, por ejemplo con SQL Server Express. La conexión está en `GUI\Config\App.config`:

```xml
<add name="TP_Ing_Soft"
     connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TP_Ing_Soft;Integrated Security=True"
     providerName="System.Data.SqlClient" />
```

Solo se cambia el `Data Source`:

| Caso | `Data Source` |
|---|---|
| LocalDB (por defecto) | `(localdb)\MSSQLLocalDB` |
| SQL Server Express | `.\SQLEXPRESS` |
| Instancia predeterminada | `.` |

En ese caso, `script.sql` se ejecuta contra esa misma instancia.

---

## 5. Envío de comprobantes por correo (opcional)

El sistema funciona sin configurar el correo; solo no envía el comprobante de la reserva por mail.

Para habilitarlo:
1. Copiar `GUI\Config\Correo.config.template` como `GUI\Config\Correo.config`.
2. Completar la casilla y la clave de aplicación.

Ese archivo nunca se sube al repositorio.

---

## 6. Problemas frecuentes

| Síntoma | Qué hacer |
|---|---|
| Errores de compilación con `FontAwesome.Sharp` o `IconChar` | Falta el paquete NuGet: ver el paso 3 de la sección 1.2. |
| «No se pudo preparar la base de datos» al iniciar | Falta LocalDB: instalarlo desde el Visual Studio Installer. Si está instalado, crear e iniciar la instancia con `sqllocaldb create MSSQLLocalDB -s`. |
| No existe el usuario `Nahu` | La aplicación creó una base vacía porque se abrió antes de correr `script.sql`. Ver la nota «Importante» de la sección 1.2. |
| SSMS no se conecta a `(localdb)\MSSQLLocalDB` | Ejecutar `sqllocaldb create MSSQLLocalDB -s`, o abrir la aplicación una vez para que cree la instancia. Después, borrar la base vacía que creó y correr `script.sql`. |
| El check-in dice que «todavía no llegó la fecha de ingreso» | La reserva tiene que empezar hoy (ver 3.0). |
| No aparecen habitaciones para el cambio pedido por el huésped | Tiene que haber una habitación de tarifa mayor libre hasta el egreso, y la estadía tiene que tener al menos una noche por delante. |
| Aviso de inconsistencia de dígitos verificadores | Se modificaron datos por fuera de la aplicación. Ingresar con `Nahu` y recalcular desde la pantalla de integridad. |
