using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using rentMyJunk.Providers;

namespace rentMyJunk.Models
{
    public class UserRepository : DocumentDb
    {
        //each repo can specify it's own database and document collection
        public UserRepository() : base("rentanything", "User") { }

        //Gets a list of all users.
        public Task<List<User>> GetUserAsync()
        {
            return Task<List<User>>.Run(() =>
                Client.CreateDocumentQuery<User>(Collection.DocumentsLink)
                .ToList());
        }

        //Gets a user by their id.
        public Task<Request> GetUserAsync(string id)
        {
            return Task<User>.Run(() =>
                Client.CreateDocumentQuery<Request>(Collection.DocumentsLink)
                .Where(r => r.id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        //Saves a new user
        public Task<ResourceResponse<Document>> SaveUser(User user)
        {
            //save the item to docuemntdb.
            return Client.CreateDocumentAsync(Collection.DocumentsLink, user);
        }

        //Updates a user.
        public Task<ResourceResponse<Document>> UpdateUserAsync(User user)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == user.id)
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, user);
        }

        //Deletes a user by their id.
        public Task<ResourceResponse<Document>> DeleteUserAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }
        
    }
}