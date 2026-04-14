using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Google.Cloud.Firestore;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace IDPBookApp.DataBase;
public class FirebaseConnecty
{
    public static FirebaseAuthConfig config = new()
    {
        ApiKey = "AIzaSyCDhlYSIr_Ic07Ppa_xDYEOl3WiktvSEEs",
        AuthDomain = "idpbook-lafe-umh.firebaseapp.com",
        Providers =
        [
            new GoogleProvider().AddScopes("email"),
            new EmailProvider()
        ],
    };

    private FirestoreDb db;
    public async Task SetupFirestore()
    {
        if (db == null)
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("idpbook_adminsdk.json");
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();
            db = new FirestoreDbBuilder
            {
                ProjectId = "idpbook-lafe-umh",
                JsonCredentials = contents
            }.Build();
        }
    }

    public readonly FileUserRepository MedRepo = new("MedUsers");
    public readonly FileUserRepository PacRepo = new("ListPac");
    public UserInfo userInfo;
    public UserInfo pacInfo;
    private FirebaseCredential firebaseCredential;
    private FirebaseCredential firebaseCredential2;
    public UserCredential firebaseUserCredential;
    private readonly FirebaseAuthClient client = new(config);
    private int cntr = -1;
    private string email = "ejem@gmail.com";

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
            email = username;            
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
    public async Task SavePac<T>(string uid,T modelo)
    {
        try
        {
            await db.Collection("IDPbookDB").Document(uid).SetAsync(modelo);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("SavePac", $"Error:\n\n {ex.Message}", "Ok");
        }
    }
    public async Task RegistMed(string email, string password, string name, int centro)
    {
        firebaseUserCredential = await client.CreateUserWithEmailAndPasswordAsync(email, password, name);
        await db.Collection("MedUsers").AddAsync(new {Correo = email, Cntr = centro});
        
        var apikey = config.ApiKey;
        var requestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + apikey;
        var idtoken = client.User.GetIdTokenAsync().Result;
        using var cliente = new HttpClient();
        cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var content = new StringContent("{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + idtoken + "\"}");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await cliente.PostAsync(requestUri, content);
        response.EnsureSuccessStatusCode();
    }
    public async Task<bool> CheckUser()
    {        
        if (MedRepo.UserExists() || PacRepo.UserExists())
        {
            (pacInfo, firebaseCredential2) = PacRepo.ReadUser();            
            (userInfo, firebaseCredential) = MedRepo.ReadUser();
            email = userInfo.Email;
            var name = userInfo.DisplayName;            
            //Falta asignar "userCredential" a "client", porque el metodo de SignOut() no reconoce ningun objeto, es decir no se ha inicado sesión explicitamente, si no por el repositorio.
            await Shell.Current.GoToAsync(nameof(MainPage));
            await Shell.Current.DisplayAlert("Bienvenido(a)", "Hola " + name + ", has iniciado sesión correctamente.", "Ok");
            return true;
        }
        else
        {
            await Shell.Current.DisplayAlert("Aviso", "Sesión caducada", "Ok");
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
                    "cuestionarios",
                    "episodios",
                    "historial",
                    "otrosTrat",
                    "tratActual",
                    "tratamientos",
                    "vacunas"
                };
                foreach (var item in lista)
                {
                    var snapshot = await db.Collection("IDPbookDB/" + firebaseUserCredential.User.Info.Uid + "/"+item).GetSnapshotAsync();
                    foreach (var document in snapshot.Documents)
                    {
                        if (item == "tratamientos" || item == "tratActual")
                        {
                            // Elimina subcolección "administraciones" si existe
                            var adminSnapshot = await document.Reference.Collection("administraciones").GetSnapshotAsync();

                            foreach (var adminDoc in adminSnapshot.Documents)
                            {
                                await adminDoc.Reference.DeleteAsync(); // Elimina cada documento dentro de "administraciones"
                            }
                        }
                        await document.Reference.DeleteAsync();
                    }
                }                
                await db.Collection("IDPbookDB").Document(firebaseUserCredential.User.Info.Uid).DeleteAsync();

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
        var query = await db.Collection("IDPbookDB").WhereEqualTo("Correo", correo).GetSnapshotAsync();
        var pacientes = new List<Paciente>();
        foreach (var paciente in query.Documents)
        {
            pacientes.Add(paciente.ConvertTo<Paciente>());
        }
        firebaseUserCredential = await client.SignInWithEmailAndPasswordAsync(correo,pacientes[0].Pass);
        await firebaseUserCredential.User.ChangePasswordAsync(password);
        await db.Collection("IDPbookDB").Document(firebaseUserCredential.User.Uid).UpdateAsync("Pass", password);
        //client.SignOut();
    }
    public async Task<QuerySnapshot> GetMedUsers(string email)
    {
        try
        {
            var count = await db.Collection("MedUsers").WhereEqualTo("Correo", email).GetSnapshotAsync();
            return count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("GetMedUsers", $"Error:\n\n{ex.Message}", "Ok");
            return null;
        }
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
    public async Task<List<Admin>> GetAdminsModel(string id,string trat,string ruta)
    {
        try
        {
            var snapshot = await db.Collection("IDPbookDB/"+id+"/"+ruta+"/"+trat+"/administraciones").GetSnapshotAsync();
            var docs = new List<Admin>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ConvertTo<Admin>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetAdmins", $"Error: {ex.Message}", "Ok");
            return null;
        }
    }
    public async Task<List<Paciente>> GetPacientesModel()
    {
        var query = await db.Collection("MedUsers").WhereEqualTo("Correo", email).GetSnapshotAsync();
        cntr = query.Documents[0].GetValue<int>("Cntr");
        if (email == "idpbook1@gmail.com" || email == "catnogmun@gmail.com")
        {
            try
            {
                var data = await db.Collection("IDPbookDB").GetSnapshotAsync();
                var pacientes = new List<Paciente>();
                foreach (var paciente in data.Documents)
                {
                    pacientes.Add(paciente.ConvertTo<Paciente>());
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
        else
        {
            try
            {
                var data = await db.Collection("IDPbookDB").WhereEqualTo("Cntr", cntr).GetSnapshotAsync();
                var pacientes = new List<Paciente>();
                foreach (var paciente in data.Documents)
                {
                    pacientes.Add(paciente.ConvertTo<Paciente>());
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
    }

    //public async Task<List<Paciente>> GetPacientesCntrModel()
    //{
    //    try
    //    {            
    //        var data = await db.Collection("IDPbookDB").WhereEqualTo("Cntr", cntr).GetSnapshotAsync();
    //        var pacientes = new List<Paciente>();
    //        foreach (var paciente in data.Documents)
    //        {
    //            pacientes.Add(paciente.ConvertTo<Paciente>());
    //        }
    //        return pacientes;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex);
    //        await Shell.Current.DisplayAlert("GetPacientesModel", $"No se pudo accerder: {ex.Message}", "Ok");
    //        return null;
    //    }
    //}

    public async Task<Paciente> GetPacienteModel(string idPac)
    {
        try
        {
            var datos = await db.Collection("IDPbookDB").Document(idPac).GetSnapshotAsync();
            var paciente = datos.ConvertTo<Paciente>();
            
            return paciente;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetPaciente", $"No se pudo obtener paciente: {ex.Message}", "Ok");
            return null;
        }
    }
    public async Task<List<T>> GetModelList<T>(string uid,string tipo)
    {
        try
        {
            var snapshot = await db.Collection("IDPbookDB/"+uid+"/"+tipo).GetSnapshotAsync();
            var docs = new List<T>();
            foreach (var doc in snapshot.Documents)
            {
                docs.Add(doc.ConvertTo<T>());
            }
            return docs;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("GetModelList", $"Error: {ex.Message}", "Ok");
            return null;
        }
    }
    public async Task SaveData<T>(string uid, string tipo, string id_doc, T modelo)
    {
        try
        {
            await db.Collection("IDPbookDB/" + uid + "/" + tipo).Document(id_doc).SetAsync(modelo);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("SaveData", $"Error:\n\n {ex.Message}", "Ok");
        }        
    }
    public async Task ElimDocs(string uid, string tipo, string id_doc)
    {
        try
        {
            await db.Collection("IDPbookDB/" + uid + "/" + tipo).Document(id_doc).DeleteAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ElimData", $"Error:\n\n {ex.Message}", "Ok");
        }
    }
    public async Task EliminarTrat(string uid,string tipo,string documentId)
    {
        try
        {
            var documentReference = db.Collection("IDPbookDB/" + uid + "/" + tipo).Document(documentId);
            var adminCollection = documentReference.Collection("administraciones");
            var adminDocuments = await adminCollection.GetSnapshotAsync();

            foreach (var adminDoc in adminDocuments.Documents)
            {
                // Eliminar cada documento de la subcolección "administraciones"
                await adminCollection.Document(adminDoc.Id).DeleteAsync();
            }

            // Después de eliminar la subcolección, eliminamos el documento principal
            await documentReference.DeleteAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Eliminar Trat.", $"No se pudo eliminar:\n\n {ex.Message}", "Ok");
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
