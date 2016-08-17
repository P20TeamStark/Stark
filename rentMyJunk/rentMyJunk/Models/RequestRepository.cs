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
    public class RequestRepository : DocumentDb
    {
        //each repo can specify it's own database and document collection
        public RequestRepository() : base("rentanything", "Request") { }

        //Gets a list of all pieces of junk.
        public Task<List<Request>> GetItemsAsync()
        {
            return Task<List<Request>>.Run(() =>
                Client.CreateDocumentQuery<Request>(Collection.DocumentsLink)
                .ToList());
        }

        //Gets a piece of junk by its id.
        public Task<Request> GetRequestAsync(string id)
        {
            return Task<Item>.Run(() =>
                Client.CreateDocumentQuery<Request>(Collection.DocumentsLink)
                .Where(r => r.id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        //Saves a new piece of junk
        public Task<ResourceResponse<Document>> SaveRequest(Request request)
        {
            request.id = System.Guid.NewGuid().ToString();
            //save the item to docuemntdb.
            return Client.CreateDocumentAsync(Collection.DocumentsLink, request);
        }

        //Updates a piece of junk.
        public Task<ResourceResponse<Document>> UpdateRequestAsync(Request request)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == request.id)
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, request);
        }

        //Deletes a piece of junk by its id.
        public Task<ResourceResponse<Document>> DeleteRequestAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }

        //Returns a list of junk for a given category name.
        public Task<List<Request>> GetRequestsByRequester(string requesterId)
        {
            return Task.Run(() =>
                Client.CreateDocumentQuery<Request>(Collection.DocumentsLink)
                .Where(i => i.requesterId == requesterId)
                .ToList());
        }


        //Returns a list of junk for a given category name.
        public Task<List<Request>> GetRequestsByOwner(string ownerId)
        {
            return Task.Run(() =>
                Client.CreateDocumentQuery<Request>(Collection.DocumentsLink)
                .Where(r => r.ownerId == ownerId)
                .ToList());
        }
    }
}