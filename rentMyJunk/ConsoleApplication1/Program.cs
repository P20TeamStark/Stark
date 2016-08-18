using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rentMyJunk.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UserRepository ur = new UserRepository();
            Task t;
            /*
            User u = new User();
            u.id = "cmitchell@hardnox.onmicrosoft.com";
            u.name = "Chris Mitchell";
            u.address = "Macon, GA";
            t = ur.SaveUser(u);
            t.Wait();

            u.id = "Bret@hardnox.onmicrosoft.com";
            u.name = "Bret Stateham";
            u.address = "San Diego, CA";
            t = ur.SaveUser(u);
            t.Wait();

            u.id = "Dustin@hardnox.onmicrosoft.com";
            u.name = "Dustin Gallegos";
            u.address = "Miami, FL";
            t = ur.SaveUser(u);
            t.Wait();

            u.id = "bob@hardnox.onmicrosoft.com";
            u.name = "Bob Jacobs";
            u.address = "Philadelphia, PA";
            t = ur.SaveUser(u);
            t.Wait();

            u.id = "paman@hardnox.onmicrosoft.com";
            u.name = "Pat Weikle";
            u.address = "Atlanta, GA";
            t = ur.SaveUser(u);
            t.Wait();
            */
            
            ItemRepository ir = new ItemRepository();
            Item i = ir.GetItemAsync("20a5cae4-e7bc-4e44-b0c2-ded30fa67ff9").Result;

            i.description = "foo";
             /*

            i.category = "Recreational";
            i.isAvailable = "true";
            i.ownerId = "patman@hardnox.onmicrosoft.com";
            i.description = "Sweet Disco Ball";
            i.ratePerDay = "20";
            path = @"c:\projects\disco.jpg";
            fs = File.OpenRead(path);

            t = ir.SaveItem(i, fs);
            t.Wait();

            i.category = "Recreational";
            i.isAvailable = "true";
            i.ownerId = "bob@hardnox.onmicrosoft.com";
            i.description = "Fancy Espresso Machine";
            i.ratePerDay = "100";
            path = @"c:\projects\espresso.jpg";
            fs = File.OpenRead(path);

            t = ir.SaveItem(i, fs);
            t.Wait();
            */
            
            RequestRepository rr = new RequestRepository();
            /*
            Request r = new Request();
            r.itemId = "20a5cae4-e7bc-4e44-b0c2-ded30fa67ff9";
            r.ownerId = "bob@hardnox.onmicrosoft.com";
            r.requesterId = "cmitchell@hardnox.onmicrosoft.com";
            r.startDate = DateTime.Today;
            r.endDate = DateTime.Today.AddDays(5);
            t = rr.SaveRequest(r);
            t.Wait();


            r.itemId = "20a5cae4-e7bc-4e44-b0c2-ded30fa67ff9";
            r.ownerId = "bob@hardnox.onmicrosoft.com";
            r.requesterId = "patman@hardnox.onmicrosoft.com";
            r.startDate = DateTime.Today;
            r.endDate = DateTime.Today.AddDays(2);
            t = rr.SaveRequest(r);
            t.Wait();
            */

            
        }
    }
}
