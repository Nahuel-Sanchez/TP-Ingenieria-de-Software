using BE_08YS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_08YS.Recepcionista
{
    public static class EstiloEstadoHabitacion_68SA
    {
        public static (string Texto, Color Fondo, Color Acento) Get(EstadoHabitacion estado)
        {
            switch (estado)
            {
                case EstadoHabitacion.Disponible:
                    return ("DISPONIBLE", Color.FromArgb(20, 58, 44), Color.FromArgb(76, 217, 100));
                case EstadoHabitacion.Reservada:
                    return ("RESERVADA", Color.FromArgb(20, 40, 70), Color.FromArgb(90, 155, 235));
                case EstadoHabitacion.Ocupada:
                    return ("OCUPADA", Color.FromArgb(60, 22, 30), Color.FromArgb(235, 90, 90));
                case EstadoHabitacion.EnLimpieza:
                    return ("EN LIMPIEZA", Color.FromArgb(58, 44, 10), Color.FromArgb(230, 175, 46));
                default: // FueraDeServicio
                    return ("FUERA DE SERVICIO", Color.FromArgb(45, 48, 55), Color.FromArgb(150, 155, 165));
            }
        }
    }
}
