using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PracticaUsuarios
{
	public class UserApiClient
	{
		private readonly HttpClient _client;

		/// <summary>
		/// Inicializa una nueva instancia de la clase <see cref="UserApiClient"/>.
		/// </summary>
		public UserApiClient()
		{
			this._client = new HttpClient();
		}

		/// <summary>
		/// Obtiene un usuario aleatorio de la API y muestra sus detalles en la consola.
		/// </summary>
		public async Task GetUserAndPrintDetails()
		{
			// Realiza una solicitud HTTP GET a la API de usuarios aleatorios
			HttpResponseMessage response = await this._client.GetAsync("https://randomuser.me/api/");

			// Verifica si la solicitud fue exitosa
			if (response.IsSuccessStatusCode)
			{
				// Configura las opciones del deserializador JSON
				JsonSerializerOptions jsonOptions = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				// Lee el contenido de la respuesta HTTP como un flujo de datos de manera asincrónica.
				Stream contentStream = await response.Content.ReadAsStreamAsync();

				// Deserializa de manera asincrónica el contenido del flujo de datos en un objeto de tipo UserResponse.
				UserResponse userResponse = await JsonSerializer.DeserializeAsync<UserResponse>(contentStream, jsonOptions);



				// Verifica si la respuesta contiene datos de usuario y si hay al menos un resultado
				if (userResponse != null && userResponse.Results.Count > 0)
				{
					// Obtiene el primer usuario aleatorio de la respuesta
					UserResult randomUser = userResponse.Results[0];

					/// <summary>
					/// Convierte la fecha de nacimiento de la respuesta JSON en un objeto DateTime.
					/// </summary>
					/// <remarks>
					/// La fecha de nacimiento se obtiene de la propiedad Date del objeto Dob en la respuesta JSON.
					/// Se utiliza el formato "yyyy-MM-ddTHH:mm:ss.fffZ" para el parseo.
					/// </remarks>

					DateTime dateOfBirth = DateTime.ParseExact(randomUser.Dob.Date, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);



					// Crea un objeto User con los detalles del usuario aleatorio
					User user = new User
					{
						Gender = randomUser.Gender,
						Title = randomUser.Name.Title,
						FirstName = randomUser.Name?.First,
						LastName = randomUser.Name?.Last,
						Email = randomUser.Email,
						Username = randomUser.Login?.Username,
						DateOfBirth = dateOfBirth.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
						Address = randomUser.Location != null ?
						 $"{randomUser.Location.Street.Number} {randomUser.Location.Street.Name}, {randomUser.Location.City}, {randomUser.Location.State}, {randomUser.Location.Country}" : null
					};

					// Imprime los detalles del usuario en la consola
					PrintUserDetails(user);


					// Crear una instancia de UserFileManager para manejar la gestión de archivos de usuario
					UserFileManager userFileManager = new UserFileManager();

					// Llamar al método SaveUserToFile para guardar los datos del usuario en un archivo
					await userFileManager.SaveUserToFile(user, randomUser.DocumentNumber);
				}
				else
				{
					// Si no se encontraron usuarios en la respuesta, muestra un mensaje en la consola
					Console.WriteLine("No se encontraron usuarios en la respuesta.");
				}
			}
			else
			{
				// Si la solicitud no fue exitosa, muestra un mensaje de error en la consola
				Console.WriteLine("Error al realizar la solicitud: " + response.ReasonPhrase);
			}
		}

		private void PrintUserDetails(User user)
		{
			Console.WriteLine("================================================================");
			Console.WriteLine("                   Datos del usuario guardados:");
			Console.WriteLine($" Género: {user.Gender}");
			Console.WriteLine($" Título: {user.Title}");
			Console.WriteLine($" Nombre: {user.FirstName} {user.LastName}");
			Console.WriteLine($" Apellido: {user.LastName}");
			Console.WriteLine($" Correo electrónico: {user.Email}");
			Console.WriteLine($" Nombre de usuario: {user.Username}");
			Console.WriteLine($" Fecha de nacimiento: {user.DateOfBirth}");
			Console.WriteLine($" Dirección: {user.Address}");
			Console.WriteLine("================================================================");
		}






		/// <summary>
		/// Obtiene el top 10 de los usuarios más antiguos por fecha de nacimiento.
		/// </summary>
		/// <param name="users">La lista de usuarios.</param>
		/// <returns>El top 10 de los usuarios más antiguos.</returns>

	}
}
