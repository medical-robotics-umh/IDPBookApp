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
        ApiKey = "AIzaSyCDhlYSIr_Ic07Ppa_xDYEOl3WiktvSEEs",
        AuthDomain = "idpbook-lafe-umh.firebaseapp.com",
        Providers = new FirebaseAuthProvider[]
        {
            new GoogleProvider().AddScopes("email"),
            new EmailProvider()
        },
    };

    public readonly FileUserRepository MedRepo = new("MedUsers");
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
        await CrossCloudFirestore.Current
                                    .Instance
                                    .Collection("MedUsers")
                                    .Document()
                                    .SetAsync(new { Correo = username });
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
    public async Task<bool> CheckUser()
    {
        if (MedRepo.UserExists() || PacRepo.UserExists())
        {
            (pacInfo, firebaseCredential2) = PacRepo.ReadUser();            
            (userInfo, firebaseCredential) = MedRepo.ReadUser();
            var name = userInfo.DisplayName;
            //Falta asignar "userCredential" a "client", porque el metodo de SignOut() no reconoce ningun objeto, es decir no se ha inicado sesión explicitamente, si no por el repositorio.
            await Shell.Current.GoToAsync(nameof(MainPage));
            await App.Current.MainPage.DisplayAlert("Bienvenido(a)", "Hola " + name + ", has iniciado sesión correctamente.", "Ok");
            return true;
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Aviso", "Sesión caducada", "Ok");
            return false;
        }
    }
    public async Task ChangePac(string username, string password, bool ban)
    {
        if (ban == true)
        {
            PacRepo.DeleteUser();
            firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(username, password);
            PacRepo.SaveUser(firebaseUserCredential.User);
            (pacInfo, firebaseCredential2) = PacRepo.ReadUser();
        }
        else
        {
            try
            {
                PacRepo.DeleteUser();
                firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(username, password);
                
                var lista = new List<string>
                {
                    "analiticas",
                    "episodios",
                    "historial",
                    "tratamientos",
                    "vacunas"
                };
                foreach (var item in lista)
                {
                    var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection("/IDPbookDB/" + firebaseUserCredential.User.Info.Uid + "/"+item)
                                                    .GetAsync();
                    foreach (var document in snapshot.Documents)
                    {
                        await document.Reference.DeleteAsync();
                    }
                }                
                await CrossCloudFirestore.Current
                         .Instance
                         .Collection("/IDPbookDB")
                         .Document(firebaseUserCredential.User.Info.Uid)
                         .DeleteAsync();

                await firebaseUserCredential.User.DeleteAsync();
                var anonimo = await client.SignInAnonymouslyAsync();
                PacRepo.SaveUser(anonimo.User);
                (pacInfo, firebaseCredential2) = PacRepo.ReadUser();
                await anonimo.User.DeleteAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ChangePac", $"No se pudo eliminar: {ex.Message}", "Ok");
            }            
        }
    }
    public void LogOut()
    {
        MedRepo.DeleteUser();
        PacRepo.DeleteUser();
        //client.SignOut();
    }
    public async Task ChangePasswordAsync(string correo,string password)
    {
        var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
                                     .WhereEqualsTo("Correo", correo)
                                     .GetAsync();
        var pacientes = new List<Paciente>();
        foreach (var paciente in query.Documents)
        {
            pacientes.Add(paciente.ToObject<Paciente>());
        }
        firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(correo,pacientes[0].Pass);
        await firebaseUserCredential.User.ChangePasswordAsync(password);
        await CrossCloudFirestore.Current
                      .Instance
                      .Collection("IDPbookDB")
                      .Document(firebaseUserCredential.User.Uid)
                      .UpdateAsync("Pass", password);
        //client.SignOut();
    }
    public async Task SendEmailAsync(string correo)
    {
        var apikey = config.ApiKey;
        var requestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + apikey;
        using (var cliente = new HttpClient())
        {
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent("{\"email\":\"" + correo + "\",\"requestType\":\"PASSWORD_RESET\"}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await cliente.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();
        }
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
    public static async Task<List<Admin>> GetAdminsModel(string id,string trat)
    {
        try
        {
            var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection("/IDPbookDB/" + id + "/tratActual/"+trat+"/administraciones")
                                                    .GetAsync();

            var docs = new List<Admin>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ToObject<Admin>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetAdmins", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }
    public static async Task<List<Paciente>> GetPacientesModel()
    {
        try
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
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
    public static async Task<List<Tratamiento>> GetTratInmunoModel(string idPac,string trat)
    {
        try
        {
            var datos = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("/IDPbookDB")
                                     .Document(idPac)
                                     .Collection(trat)
                                     .WhereGreaterThanOrEqualsTo("TId", "ITrat")
                                     .GetAsync();

            var tratamientos = new List<Tratamiento>();
            foreach (var item in datos.Documents)
            {
                tratamientos.Add(item.ToObject<Tratamiento>());
            }

            return tratamientos;
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
                                     .WhereGreaterThanOrEqualsTo("Id","OTrat")
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
    public static async Task<List<Vacuna>> GetVacunasModel(string ruta)
    {
        try
        {
            var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection("/IDPbookDB/"+ruta+"/vacunas")
                                                    .GetAsync();

            var docs = new List<Vacuna>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ToObject<Vacuna>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetVacunasModel", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }
    public static async Task<List<Cuestionario>> GetCuestionariosModel(string ruta)
    {
        try
        {
            var snapshot = await CrossCloudFirestore.Current.Instance
                                                    .Collection("/IDPbookDB/" + ruta + "/cuestionarios")
                                                    .GetAsync();

            var docs = new List<Cuestionario>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ToObject<Cuestionario>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetCuestionariosModel", $"No se pudo accerder: {ex.Message}", "Ok");
            return null;
        }
    }
    public static async Task ElimData(string uid,string tipo, string ruta)
    {
        try
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("/IDPbookDB")
                         .Document(uid)
                         .Collection(tipo)
                         .Document(ruta)
                         .DeleteAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Eliminar Docs", $"No se pudo eliminar:\n\n {ex.Message}", "Ok");
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
}
