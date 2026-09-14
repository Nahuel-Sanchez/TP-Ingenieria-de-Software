// ConfiguracionHotel_68SA.cs
using System;

namespace BE_08YS
{
    public class ConfiguracionHotel_68SA
    {
        public int Id { get; set; }
        public TimeSpan HoraCheckIn { get; set; }
        public TimeSpan HoraCheckOut { get; set; }
        public decimal GraciaNoShowHoras { get; set; }
    }
}