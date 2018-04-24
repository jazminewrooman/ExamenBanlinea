using System;
using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExamenBanlinea.Models.DTOs;
using System.Net.Http;

namespace ExamenBanlinea.Services
{
    public interface IContactsApi
    {
        [Get("/api/countries")]
        Task<List<Countries>> GetCountries();

        [Post("/api/ContactRegister")]
        Task<HttpResponseMessage> SaveContacts([Body] ContactsRequest req);
    }
}
