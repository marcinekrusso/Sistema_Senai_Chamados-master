namespace Senai.Chamados.Web.ViewModels.Grafico
{
    public class ListaGraficoViewModel
    {
        public GraficoViewModel GraficoStatus { get; set; }
        public GraficoViewModel GraficoSetor { get; set; }
        
        public ListaGraficoViewModel()
        {
            GraficoStatus = new GraficoViewModel();
            GraficoSetor = new GraficoViewModel();
        }
    }
}