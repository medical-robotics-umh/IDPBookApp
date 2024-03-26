using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace IDPBookApp.DataBase;
public class FirebaseConnecty
{
    public static FirebaseAuthConfig config = new()
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
    public readonly FileUserRepository PacRepo = new("ListPac");
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
        PacRepo.SaveUser(firebaseUserCredential.User);
        (pacInfo,firebaseCredential2) = PacRepo.ReadUser();    
    }
    public async Task RegistMed(string username, string password, string name)
    {
        firebaseUserCredential = await client.CreateUserWithEmailAndPasswordAsync(username, password, name);
        var apikey = config.ApiKey;
        var requestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + apikey;
        var idtoken = client.User.GetIdTokenAsync().Result;
        using (var cliente = new HttpClient())
        {
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent("{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + idtoken + "\"}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await cliente.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();
        }
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

    public async Task ChangePac(string username, string password)
    {
        PacRepo.DeleteUser();
        firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(username, password);
        PacRepo.SaveUser(firebaseUserCredential.User);
        (pacInfo, firebaseCredential2) = PacRepo.ReadUser();
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
                                                    .Collection("/IDPbookDB/"+rutaEpis+"/episodios")
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
            await Shell.Current.DisplayAlert("GetEpisodiosModel", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }

    public static async Task<List<HistoriaModel>> GetHistoriasModel(string ruta)
    {
        try
        {
            var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection("/IDPbookDB/" + ruta + "/historial")
                                                    .GetAsync();

            var docs = new List<HistoriaModel>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ToObject<HistoriaModel>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetEpisodiosModel", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }

    public static async Task<List<AnaliticaModel>> GetAnaliticsModel(string ruta)
    {
        try
        {
            var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection("/IDPbookDB/" + ruta + "/analiticas")
                                                    .GetAsync();

            var docs = new List<AnaliticaModel>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ToObject<AnaliticaModel>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetAnaliticsModel", $"No se pudo accerder: {ex.Message}", "Ok");
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
            await Shell.Current.DisplayAlert("GetPacientesModel", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }

    public static async Task<Paciente> GetPacienteModel(string idPac)
    {
        try
        {
            var datos = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
                                     .Document(idPac)
                                     .GetAsync();

            var paciente = datos.ToObject<Paciente>();
            
            return paciente;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetPaciente", $"No se pudo obtener paciente: {ex.Message}", "Ok");
            return null;
        }
    }

    public static async Task<Tratamiento> GetTratInmunoModel(string idPac)
    {
        try
        {
            var datos = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
                                     .Document(idPac)
                                     .Collection("tratamientos")
                                     .Document("InmunoActual")
                                     .GetAsync();

            var tratamiento = datos.ToObject<Tratamiento>();

            return tratamiento;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetTratInmuno", $"No se pudo obtener documento: {ex.Message}", "Ok");
            return null;
        }
    }

    public static async Task<List<OtroTrat>> GetOtrosTratModel(string idPac)
    {
        try
        {
            var datos = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
                                     .Document(idPac)
                                     .Collection("tratamientos")
                                     .WhereEqualsTo("Id","OTrat")
                                     .GetAsync();

            var tratamientos = new List<OtroTrat>();
            foreach (var otroTrat in datos.Documents)
            {
                tratamientos.Add(otroTrat.ToObject<OtroTrat>());
            }
            return tratamientos;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetPaciente", $"No se pudo obtener paciente: {ex.Message}", "Ok");
            return null;
        }
    }
}
