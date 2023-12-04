using Firebase.Auth.Providers;
using Firebase.Auth;

namespace IDPBookApp.DataBase;
public class FirebaseConnecty
{
    public FirebaseAuthClient ConectarFirebase()
    {
        var config = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyCrqRG1QeBhVY9hRATjaqRZ8Cw_fqEjBwo",
            AuthDomain = "testdb-9da53.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
                    {
                    new GoogleProvider().AddScopes(),
                    new EmailProvider()
                    },
        };
        var cliente = new FirebaseAuthClient(config);

        return cliente;
    }
}
