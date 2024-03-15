using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;
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
            new GoogleProvider().AddScopes("email"),
            new EmailProvider()
        },
    };

    private readonly FileUserRepository MedRepo = new("MedUsers");
    private readonly FileUserRepository PacRepo = new("ListPac");
    public UserInfo userInfo;
    public UserInfo pacInfo;
    private FirebaseCredential firebaseCredential;
    private FirebaseCredential firebaseCredential2;
    public UserCredential firebaseUserCredential;
    private readonly FirebaseAuthClient client = new(config);

    public async Task Login(string username, string password)
    {
        firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(username, password);
        if(firebaseUserCredential.User.Info.IsEmailVerified==true)
        {
            MedRepo.SaveUser(firebaseUserCredential.User);
            (userInfo, firebaseCredential) = MedRepo.ReadUser();
            var anonimo = await client.SignInAnonymouslyAsync();
            PacRepo.SaveUser(anonimo.User);
            (pacInfo, firebaseCredential2) = PacRepo.ReadUser();
            await anonimo.User.DeleteAsync();
        }
        else
        {
            PacRepo.SaveUser(firebaseUserCredential.User);
            (pacInfo, firebaseCredential2) = PacRepo.ReadUser();
            MedRepo.SaveUser(firebaseUserCredential.User);
            (userInfo, firebaseCredential) = MedRepo.ReadUser();
        }        
    }

    public async Task RegistPac(string username, string password, string name)
    {
        firebaseUserCredential = await client.CreateUserWithEmailAndPasswordAsync(username, password, name);
        MedRepo.SaveUser(firebaseUserCredential.User);
        (pacInfo,firebaseCredential2) = MedRepo.ReadUser();
    }

    //public async Task<UserCredential> LoginWithGoogle()
    //{
    //    var url = "";
    //    var provider = new GoogleProvider();
    //    try
    //    {            
    //        WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
    //            new WebAuthenticatorOptions()
    //            {
    //                Url = new Uri(url),
    //                CallbackUrl = new Uri("IDPBookApp://"),
    //                PrefersEphemeralWebBrowserSession = true
    //            });
    //        string accessToken = result?.AccessToken;
    //    }
    //    catch(TaskCanceledException e)
    //    {
    //        await Shell.Current.DisplayAlert("Error", $"WebAuthenticator: {e.Message}", "Ok");
    //    }
    //    try
    //    {
    //        var userCredential = await client.SignInWithRedirectAsync(provider.ProviderType, async url =>
    //        { 
    //            Uri uri = new(url);
    //            var response = await Browser.Default.OpenAsync(uri,BrowserLaunchMode.SystemPreferred);
    //            return "";
    //        });

    //    }
    //    catch (Exception ex) 
    //    {
    //        await Shell.Current.DisplayAlert("Error", $"No se pudo obtener la cuenta Google: {ex.Message}", "Ok");
    //    }
    //    return null;
    //}

    public async void CheckUser()
    {
        if (MedRepo.UserExists() || PacRepo.UserExists())
        {
            (pacInfo, firebaseCredential2) = PacRepo.ReadUser();
            (userInfo, firebaseCredential) = MedRepo.ReadUser();
            var name = userInfo.DisplayName;
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
        MedRepo.DeleteUser();
        PacRepo.DeleteUser();
        //client.SignOut();
    }

    public void ChangePassword(string password)
    {
        firebaseUserCredential.User.ChangePasswordAsync(password);
    }

    public static async Task<List<EpisodioModel>> GetEpisodiosModel(string rutaEpis)
    {
        try
        {
            var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection(rutaEpis)
                                                    .GetAsync();

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
            await Shell.Current.DisplayAlert("Episodios", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }

    public static async Task<List<Paciente>> GetPacientesModel(string idMed)
    {
        try
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
                                     .WhereEqualsTo("IdMed", idMed)
                                     .GetAsync();

            var pacientes = new List<Paciente>();
            foreach (var paciente in query.Documents)
            {
                pacientes.Add(paciente.ToObject<Paciente>());
            }
            return pacientes;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Pacientes", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }
}
