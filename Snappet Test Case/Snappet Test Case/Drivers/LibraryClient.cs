using Snappet_Test_Case.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Snappet_Test_Case.Drivers
{
    public static class LibraryClient
    {
        public static ClientResponse ClientId;
        public static ClientRequest ClientRequest;
        public static LoginResponse LoginResponse;

        private static readonly string shelfApiEndpoint = "https://boyns97jh5.execute-api.eu-west-1.amazonaws.com/Prod/api/v1/Shelf";
        private static readonly string bookApiEndpoint = "https://boyns97jh5.execute-api.eu-west-1.amazonaws.com/Prod/api/v1/Book";

        private static List<Shelf> shelfs = new List<Shelf>();
        private static List<Book> books = new List<Book>();

        public static async Task<LoginResponse> LoginUserAsync(string user, string password, string scope)
        {
            string loginUrl = "https://boyns97jh5.execute-api.eu-west-1.amazonaws.com/Prod/connect/token";
            var loginUser = new LoginRequest()
            {
                username = user,
                password = password,
                scope = scope,
                client_id = ClientId.id,
                client_secret = ClientRequest.secret,
                grant_type = "password"
            };
            var response = await RequestsClient.GetAuthorizeToken(loginUrl, loginUser);
            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(response);
            return LoginResponse;
        }

        public static async Task CreateShelfAsync(string name, string id)
        {
            var shelf = new Shelf()
            {
                name = name,
                id = id
            };
            var json = JsonSerializer.Serialize(shelf);
            var response = await RequestsClient.SendJsonPostRequestAsync(json, shelfApiEndpoint);
            shelfs.Add(JsonSerializer.Deserialize<Shelf>(response));
        }

        public static async Task CreateBook(string name, string shelfId)
        {
            var book = new Book()
            {
                name = name,
                shelfId = shelfId
            };
            var json = JsonSerializer.Serialize(book);
            var response = await RequestsClient.SendJsonPostRequestAsync(json, bookApiEndpoint);
            books.Add(JsonSerializer.Deserialize<Book>(response));
        }

        public static async Task<bool> CheckNrShelvesAndBooksAsync(int nrOfShelves, int nrOfBooks)
        {
            bool expectedAmount = true;
            var shelves = await GetAllShelfAsync();
            var books = await GetAllBooksAsync();
            if (nrOfBooks != books.books.Count)
            {
                expectedAmount = false;
            }
            if (nrOfShelves == shelves.shelves.Count)
            {
                expectedAmount = false;
            }
            return expectedAmount;
        }

        public static async Task<Shelves> GetAllShelfAsync()
        {
            var response = await RequestsClient.SendGetRequestAsync(shelfApiEndpoint);
            return JsonSerializer.Deserialize<Shelves>(response);
        }

        public static async Task<Books> GetAllBooksAsync()
        {
            var response = await RequestsClient.SendGetRequestAsync(bookApiEndpoint);
            return JsonSerializer.Deserialize<Books>(response);
        }

        public static void SetToken(LoginResponse loginResponse)
        {
            RequestsClient.SetToken(loginResponse);
        }
    }
}
