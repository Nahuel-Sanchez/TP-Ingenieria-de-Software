# Instalador de Horizon Hotel & Resort

Instalador y reinstalador en **Inno Setup 6**, con una sola pantalla para elegir:

| Opción | Qué instala | Base de datos |
|---|---|---|
| **Instalar** | Versión 1.0.0 (primer cuatrimestre): usuarios, perfiles, bitácora de eventos, backup/restore, dígitos verificadores, idioma | `Database/Deploy_TP_Ing_Soft_v1.sql`: crea `TP_Ing_Soft` si no existe; si existe no la toca (`DEPLOY_OMITIDO`) |
| **Reinstalar** | Versión 2.0.0 (actual) en la misma carpeta e instancia | `Database/Update_TP_Ing_Soft_v2.sql`: agrega lo de negocio sin perder datos (`UPDATE_OK`), con backup previo opcional |

## Carpetas

- `HorizonHotel.iss`: el instalador.
- `Database/`: los dos scripts (UTF-8, idempotentes, cada uno en una sola transacción). La app también los usa si arranca sin base (`InstaladorBD_08YS`).
- `Version1/`: binarios de la versión 1 entregada (extraídos del MSI `Setup_08YS/Release/Setup_08YS.msi`).
- `Imagenes/`: imágenes del asistente (azul y dorado de la marca) en todos los tamaños de DPI.
- `Manual/`: poner acá `Manual_de_instalacion.pdf` para que se instale con el sistema.
- `Salida/`: el `.exe` generado (no se sube al repositorio).

## Compilar

1. Compilar la solución en **Release**.
2. Tener `GUI/Config/Correo.config` (no está en el repositorio; copiar `Correo.config.template` y completar la clave de aplicación de la casilla). Si no existe, el sistema instalado no envía correos.
3. Ejecutar `Compilar.bat` (o abrir `HorizonHotel.iss` en Inno Setup 6 y Compilar).

## Cómo decide

- Detecta una instalación anterior por el registro `HKLM\Software\TPIngSoftSanchezYapura\HorizonHotelResort` (`InstallPath`, `Version`, `SqlInstance`, `Database`) o por el MSI de la versión 1; si no hay, solo se puede Instalar.
- Las instancias de SQL Server salen del registro de Windows (más LocalDB); se puede escribir otra. Hay que probar la conexión para avanzar.
- Base de datos **antes** de copiar archivos: si falla el backup o el script, se cancela sin tocar nada (los scripts deshacen sus cambios).
- Después de copiar: escribe la instancia en `GUI_08YS.exe.config` y guarda el registro.
- Logs en `C:\ProgramData\Horizon Hotel & Resort\Logs`.
- Desinstalar nunca borra la base.
