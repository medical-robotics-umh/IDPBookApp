using Firebase.Auth.Providers;
using Firebase.Auth;

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
        }
    };
    FirebaseAuthClient client = new FirebaseAuthClient(config);

    public async Task<UserCredential>Login(string username, string password)
    {
        var userCredential = await client.SignInWithEmailAndPasswordAsync(username, password);
        return userCredential;
    }
}
