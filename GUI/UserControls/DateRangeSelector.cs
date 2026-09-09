using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomControls
{
    /// <summary>Modo de selección de un CustomCalendarPanel.</summary>
    public enum CalendarSelectionMode
    {
        /// <summary>Una única fecha (comportamiento original del control).</summary>
        Single,

        /// <summary>Un período (check-in + check-out), para reservas.</summary>
        Range
    }

    /// <summary>
    /// Modelo de selección de un período de fechas, sin ninguna dependencia
    /// de UI. Puede compartirse entre varios CustomCalendarPanel (por ejemplo,
    /// dos paneles mostrando meses consecutivos) para que todos reflejen
    /// siempre el mismo período.
    /// </summary>
    public class DateRangeSelector
    {
        private DateTime _minDate = new DateTime(1753, 1, 1);
        private DateTime _maxDate = new DateTime(9998, 12, 31);

        public DateTime? Start { get; private set; }
        public DateTime? End { get; private set; }

        public DateTime MinDate { get => _minDate; set => _minDate = value.Date; }
        public DateTime MaxDate { get => _maxDate; set => _maxDate = value.Date; }

        /// <summary>Estadía mínima en noches. Por defecto 1 (no permite fin == inicio).</summary>
        public int MinNights { get; set; } = 1;

        /// <summary>Estadía máxima en noches. Null = sin límite.</summary>
        public int? MaxNights { get; set; }

        /// <summary>Fechas ya reservadas: no se pueden seleccionar ni formar parte de un período.</summary>
        public HashSet<DateTime> BlockedDates { get; } = new HashSet<DateTime>();

        /// <summary>Se dispara con cada cambio (incluye selecciones parciales).</summary>
        public event EventHandler RangeChanged;

        /// <summary>Se dispara solo cuando el período queda completo.</summary>
        public event DateRangeEventHandler RangeCompleted;

        public bool IsBlocked(DateTime date) => BlockedDates.Contains(date.Date);

        public bool IsWithinBounds(DateTime date) =>
            date.Date >= _minDate.Date && date.Date <= _maxDate.Date;

        /// <summary>Si es true, no permite seleccionar fechas anteriores a hoy (hoy sí se puede).</summary>
        public bool DisablePastDates { get; set; }

        private bool IsPastDisabled(DateTime date) => DisablePastDates && date.Date < DateTime.Today;

        public bool IsSelectable(DateTime date) =>
            IsWithinBounds(date) && !IsBlocked(date) && !IsPastDisabled(date);

        /// <summary>True si hay alguna fecha bloqueada estrictamente entre las dos dadas.</summary>
        public bool HasBlockedBetween(DateTime a, DateTime b)
        {
            if (BlockedDates.Count == 0) return false;
            DateTime from = a.Date < b.Date ? a.Date : b.Date;
            DateTime to = a.Date < b.Date ? b.Date : a.Date;
            for (DateTime d = from.AddDays(1); d < to; d = d.AddDays(1))
                if (BlockedDates.Contains(d)) return true;
            return false;
        }

        /// <summary>Aplica un click sobre la fecha indicada, siguiendo las reglas de selección.</summary>
        public void Select(DateTime date)
        {
            date = date.Date;
            if (!IsSelectable(date)) return;

            if (Start == null || End != null)
            {
                Start = date;
                End = null;
            }
            else if (date < Start.Value)
            {
                Start = date;
                End = null;
            }
            else if (date == Start.Value)
            {
                Start = null;
                End = null;
            }
            else
            {
                int nights = (date - Start.Value).Days;

                if (HasBlockedBetween(Start.Value, date))
                {
                    Start = date;
                    End = null;
                }
                else if (nights < MinNights || (MaxNights.HasValue && nights > MaxNights.Value))
                {
                    return;
                }
                else
                {
                    End = date;
                    RangeCompleted?.Invoke(this, new DateRangeEventArgs(Start.Value, End.Value));
                }
            }

            RangeChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Limpia la selección actual.</summary>
        public void Clear()
        {
            if (Start == null && End == null) return;
            Start = null;
            End = null;
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Carga un período ya conocido (ej. al editar una reserva existente),
        /// sin pasar por las validaciones de click.</summary>
        public void SetRange(DateTime start, DateTime? end)
        {
            Start = start.Date;
            End = end?.Date;
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
