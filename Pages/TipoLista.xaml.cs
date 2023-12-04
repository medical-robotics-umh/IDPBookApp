using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class TipoLista : ContentPage
{
	public TipoLista(DetailPageViewModel detailPageViewModel)
	{
		InitializeComponent();
        listaItems.ItemsSource = getItems();
        listaIconos.ItemsSource = getIcons2();
        BindingContext = detailPageViewModel;
    }

    private List<IconModel> getIcons2()
    {
        return new List<IconModel>()
        {
            new IconModel{Nombre="Agregar nueva vacuna",Icono="ic_logo_agregar.png"},
            //new IconModel{Nombre="Agregar tratamiento subcutáneo",Icono="ic_logo_inmunoglobulinas_subcutaneas.png"},
        };
    }

    private List<ItemModel> getItems()
    {

        return new List<ItemModel>()
        {
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 10"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 9"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 8"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 7"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 6"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 5"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 4"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 3"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 2"},
            new ItemModel{Fecha="10/05/23",Icono="ic_logo_vacunacion.png",Episodio="Vacuna 1"},
        };

        //return new List<ItemModel>()
        //{
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 8"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 7"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 6"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 5"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 4"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 3"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 2"},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_otros_tratamientos.png",Episodio="Tratamiento 1"},
        //};

        //return new List<ItemModel>()
        //{
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 12 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 11 - "},
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 10 - "},
        //    new ItemModel{Fecha="02/10/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 9 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 8 - "},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 7 - "},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 6 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 5 - "},
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 4 - "},
        //    new ItemModel{Fecha="02/10/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 3 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 2 - "},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_calidad_vida.png",Episodio="Cuestionario 1 - "},
        //};

        //return new List<ItemModel>()
        //{
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 12 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 11 - "},
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 10 - "},
        //    new ItemModel{Fecha="02/10/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 9 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 8 - "},x
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 7 - "},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 6 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 5 - "},
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 4 - "},
        //    new ItemModel{Fecha="02/10/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 3 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 2 - "},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_laboratorio.png",Episodio="Analítica 1 - "},
        //};

        //return new List<ItemModel>()
        //{
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_inmunoglobulinas_subcutaneas.png",Episodio="Nombre 6 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_inmunoglobulinas_intravenosas.png",Episodio="Nombre 5 - "},
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_inmunoglobulinas_intravenosas.png",Episodio="Nombre 4 - "},
        //    new ItemModel{Fecha="02/10/23",Icono="ic_logo_inmunoglobulinas_subcutaneas.png",Episodio="Nombre 3 - "},
        //    new ItemModel{Fecha="15/08/23",Icono="ic_logo_inmunoglobulinas_intravenosas.png",Episodio="Nombre 2 - "},
        //    new ItemModel{Fecha="10/05/23",Icono="ic_logo_inmunoglobulinas_subcutaneas.png",Episodio="Nombre 1 - "},
        //};

        //return new List<ItemModel>()
        //{
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_historial_medico.png",Episodio="Título 17 - "},
        //    new ItemModel{Fecha="11/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 16 - "},
        //    new ItemModel{Fecha="2/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 15 - "},
        //    new ItemModel{Fecha="18/08/23",Icono="ic_logo_historial_medico.png",Episodio="Título 14 - "},
        //    new ItemModel{Fecha="20/05/23",Icono="ic_logo_historial_medico.png",Episodio="Título 13 - "},
        //    new ItemModel{Fecha="11/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 12 - "},
        //    new ItemModel{Fecha="2/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 11 - "},
        //    new ItemModel{Fecha="18/08/23",Icono="ic_logo_historial_medico.png",Episodio="Título 10 - "},
        //    new ItemModel{Fecha="20/05/23",Icono="ic_logo_historial_medico.png",Episodio="Título 9 - "},
        //    new ItemModel{Fecha="05/12/23",Icono="ic_logo_historial_medico.png",Episodio="Título 8 - "},
        //    new ItemModel{Fecha="20/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 7 - "},
        //    new ItemModel{Fecha="30/11/23",Icono="ic_logo_historial_medico.png",Episodio="Título 6 - "},
        //    new ItemModel{Fecha="25/11/23",Icono="ic_logo_historial_medico.png",Episodio="Título 5 - "},
        //    new ItemModel{Fecha="11/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 4 - "},
        //    new ItemModel{Fecha="2/10/23",Icono="ic_logo_historial_medico.png",Episodio="Título 3 - "},
        //    new ItemModel{Fecha="18/08/23",Icono="ic_logo_historial_medico.png",Episodio="Título 2 - "},
        //    new ItemModel{Fecha="20/05/23",Icono="ic_logo_historial_medico.png",Episodio="Título 1 - "},
        //};
    }
}