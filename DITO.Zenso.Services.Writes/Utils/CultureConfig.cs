using System.Globalization;

namespace DITO.Zenso.Services.Writes
{
    /// <summary>
    /// Establece la configuración de la cultura para las aplicaciones
    /// </summary>
    public static class CultureConfig
    {
        /// <summary>
        /// Establece la cultura del sistema para la ejecución de las consultas
        /// </summary>
        public static void SetRegionalConfiguration()
        {
            System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
            SetRegionalConfiguration(ref thread);
        }
        /// <summary>
        /// Establece la configuración región para garantizar que las consultas 
        /// funcionan de un mismo modo sin importar el lenguaje del servidor de componentes.
        /// </summary>
        /// <param name="thread">Hilo de ejecución</param>
        /// <param name="culture">Cultura que se quiere configurar para el sistema</param>
       [System.Obsolete("Muchas aplicaciones vienen con la configuración Es-es, por lo tanto esto no aplica, usar sobrecarga")]
        public static void SetRegionalConfiguration(ref System.Threading.Thread thread, string culture)
        {
            if (culture == "es-CO")
            {
                NumberFormatInfo numericFormat =
                    ((NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone());

                DateTimeFormatInfo dateFormat =
                    ((DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone());

                CultureInfo cultureinfo = CultureInfo.CreateSpecificCulture(culture);

                numericFormat.NumberDecimalSeparator = ".";
                numericFormat.CurrencyDecimalSeparator = ".";
                numericFormat.NumberGroupSeparator = ",";
                numericFormat.CurrencyGroupSeparator = ",";

                dateFormat.ShortDatePattern = "dd/MM/yyyy";
                dateFormat.LongTimePattern = "HH:mm:ss tt";

                cultureinfo.DateTimeFormat = dateFormat;
                cultureinfo.NumberFormat = numericFormat;

                thread.CurrentCulture = cultureinfo;
            }
        }
       /// <summary>
       /// Establece la configuración región para garantizar que las consultas 
       /// funcionan de un mismo modo sin importar el lenguaje del servidor de componentes.
       /// </summary>
       /// <param name="thread">Hilo de ejecución</param>     
       public static void SetRegionalConfiguration(ref System.Threading.Thread thread)
       {
           NumberFormatInfo numericFormat = ((NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone());
           DateTimeFormatInfo dateFormat = ((DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone());

           CultureInfo cultureinfo = CultureInfo.CreateSpecificCulture("es-CO");

           numericFormat.NumberDecimalSeparator = ".";
           numericFormat.CurrencyDecimalSeparator = ".";
           numericFormat.NumberGroupSeparator = ",";
           numericFormat.CurrencyGroupSeparator = ",";

           dateFormat.ShortDatePattern = "dd/MM/yyyy";
           dateFormat.LongTimePattern = "HH:mm:ss tt";

           cultureinfo.DateTimeFormat = dateFormat;
           cultureinfo.NumberFormat = numericFormat;

           thread.CurrentCulture = cultureinfo;
           thread.CurrentUICulture = cultureinfo;
       }
    }
}
