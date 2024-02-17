using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Auth.Repository;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;
using IDPBookApp.Models;
using System.Diagnostics;

namespace IDPBookApp.DataBase;
public class FirebaseConnecty
{
    public static FirebaseAuthConfig config = new FirebaseAuthConfig()
    {
        ApiKey = "AIzaSyCrqRG1QeBhVY9hRATjaqRZ8Cw_fqEjBwo",
        AuthDomain = "testdb-9da53.firebaseapp.com",
        Providers = new FirebaseAuthProvider[]
        {
            new GoogleProvider().AddScopes(),
            new EmailProvider()
        },
    };

    private FileUserRepository repository = new("FirebaseSample");
    private UserInfo userInfo;
    private FirebaseCredential firebaseCredential;
    private UserCredential firebaseUserCredential;
    private readonly FirebaseAuthClient client = new FirebaseAuthClient(config);

    public async Task Login(string username, string password)
    {
        firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(username, password);
        repository.SaveUser(firebaseUserCredential.User);
    }

    public async void CheckUser()
    {
        if (repository.UserExists())
        {
            (userInfo, firebaseCredential) = repository.ReadUser();
            var name = userInfo.DisplayName.ToString();
            //Falta asignar "userCredential" a "client", porque el metodo de SignOut() no reconoce ningun objeto, es decir no se ha inicado sesión explicitamente, si no por el repositorio.
            await Shell.Current.GoToAsync(nameof(MainPage));
            await App.Current.MainPage.DisplayAlert("Bienvenid@", "Hola " + name + ", has iniciado sesión correctamente.", "Ok");
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Aviso", "Sesión caducada", "Ok");
        }
    }

    public void LogOut()
    {
        repository.DeleteUser();
        //client.SignOut();
    }

    public void ChangePassword(string password)
    {
        firebaseUserCredential.User.ChangePasswordAsync(password);
    }

    public async Task<List<EpisodioModel>> GetEpisodiosModel(string rutaEpis)
    {
        try
        {
            var query = CrossCloudFirestore.Current.Instance
                                           .Collection(rutaEpis);
            var snapshot = await query.GetAsync();

            var episodios = new List<EpisodioModel>();
            foreach (var episodio in snapshot.Documents)
            {
                episodios.Add(episodio.ToObject<EpisodioModel>());
            }
            return episodios;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"No se pudo accerder a la base de datos: {ex.Message}", "Ok");
            return null;
        }
    }
}
