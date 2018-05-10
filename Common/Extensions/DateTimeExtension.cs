using System;

namespace Common.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Convertir todas la columnas a tipo string
        /// </summary>
        /// <param name="datetime">DateTime que se convertira</param>
        /// <param name="utc">UTC al cual se convertira</param>
        /// <returns>Retorna la Fecha convertidad</returns>
        public static DateTime ConverToTimeZone(this DateTime datetime, string utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, TimeZoneInfo.FindSystemTimeZoneById(utc));
        }

        /// <summary>
        /// Convertir todas la columnas a tipo string
        /// </summary>
        /// <param name="primerFecha">Fecha Inicio</param>
        /// <param name="segundaFecha">Fecha Fin</param>
        /// <returns>Retorna la candidad de dias de diferencia</returns>
        public static int Differencedays(this DateTime primerFecha, DateTime segundaFecha)
        {
            var diferencia = primerFecha - segundaFecha;

            return diferencia.Days;
        }
    }
}
