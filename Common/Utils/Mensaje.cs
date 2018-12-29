using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
   public static class Mensaje
    {
        public static string Error { get { return "Error."; } }

        public static string Sincronizando { get { return "Sincronizando."; } }

        public static string ErrorAlConsultar { get { return "Error al consultar"; } }
        public static string CorreoNoEncontrado { get { return "El correo solicitado no se ha encontrado"; } }
        public static string OK { get { return "OK"; } }

        public static string ErrorAlSincornizar { get { return "Error al sincronizar, intente de nuevo por favor"; } }

        public static string Autenticando { get { return "Autenticando"; } }

        public static string Informacion { get { return "Información"; } }
        public static string Aceptar { get { return "Aceptar"; } }

        public static string Satisfactorio { get { return "La acción se a realizado satisfactoriamente"; } }
        
        public static string ValidarFormulario { get { return "Debe seleccionar todos los campos"; } }
    }
}
