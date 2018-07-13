using System;

namespace Senai.Chamados.Web.Helpers
{
    public class MeuHelper
    {

        public static string DireitosReservados()
        {
            return "® O direito dessa budega é minha !@#$%¨&*() " + DateTime.Now.Year;
        }
        public static string BoasVindas()
        {
            return "Seja bem vindo";
        }

    }
}