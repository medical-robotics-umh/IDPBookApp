using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NuevaAnalitica : ContentPage
{
	public NuevaAnalitica(NewAnltcViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Rango10k(object sender, TextChangedEventArgs e)
    {
		try
		{
			var value = Convert.ToInt64(e.NewTextValue);
			if (value >= 10000)
			{
				((Entry)sender).Text = "10000";
			}
		}
		catch
		{
            ((Entry)sender).Text = "";
        }
    }

    private void Rango20(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 20)
            {
                ((Entry)sender).Text = "20";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }

    private void Rango200(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 200)
            {
                ((Entry)sender).Text = "200";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }

    private void Rango500(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 500)
            {
                ((Entry)sender).Text = "500";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }

    private void Rango1k(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 1000)
            {
                ((Entry)sender).Text = "1000";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }

    private void Rango30(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 30)
            {
                ((Entry)sender).Text = "30";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }

    private void Rango100K(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 100000)
            {
                ((Entry)sender).Text = "100000";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }

    private void Rango2M(object sender, TextChangedEventArgs e)
    {
        try
        {
            var value = Convert.ToInt64(e.NewTextValue);
            if (value >= 2000000)
            {
                ((Entry)sender).Text = "2000000";
            }
        }
        catch
        {
            ((Entry)sender).Text = "";
        }
    }
}