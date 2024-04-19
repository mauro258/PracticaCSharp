using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaUsuarios
{
	public class UserFileManager
	{
		// Constantes para el directorio y el prefijo del nombre del archivo
		private const String DirectoryPath = "Users";
		private const String FileNamePrefix = "User";

		/// <summary>
		/// Guarda los datos de un usuario en un archivo de texto.
		/// </summary>
		/// <param name="user">El objeto User que contiene los datos del usuario.</param>
		/// <param name="documentNumber">El número de documento del usuario, que se utilizará como parte del nombre del archivo.</param>
		/// <returns>Una tarea que representa la operación asincrónica.</returns>
		public async Task SaveUserToFile(User user, String documentNumber)
		{
			// Ruta completa del directorio donde se guardarán los archivos
			String directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DirectoryPath);

			// Ruta completa del archivo a crear, incluyendo el nombre del archivo con el número de documento
			String filePath = Path.Combine(directoryPath, $"{FileNamePrefix}_{documentNumber}.txt");

			try
			{
				// Crea el directorio si no existe
				Directory.CreateDirectory(directoryPath);

				// Escribe los datos del usuario en el archivo de texto
				using (StreamWriter writer = File.CreateText(filePath))
				{
					// Encabezados
					await writer.WriteLineAsync("Género, Título, Nombre, Apellido, Correo electrónico, Nombre de usuario, Fecha de nacimiento, Dirección");

					// Contenido del usuario, separado por comas
					await writer.WriteLineAsync($"{user.Gender}, {user.Title}, {user.FirstName}, {user.LastName}, {user.Email}, {user.Username}, {user.DateOfBirth}, {user.Address}");
				}
				Console.WriteLine($"Los datos del usuario se han guardado en el archivo: {filePath}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al intentar guardar los datos del usuario en el archivo: {ex.Message}");
			}
		}



		public void ListUsersByName()
		{
			// Ruta completa del directorio donde se guardan los archivos
			String directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DirectoryPath);

			try
			{
				// Verificar si el directorio existe
				if (Directory.Exists(directoryPath))
				{
					// Obtener la lista de archivos de usuario
					String[] files = Directory.GetFiles(directoryPath, $"{FileNamePrefix}_*.txt");

					if (files.Any())
					{
						// Iterar sobre cada archivo
						foreach (String file in files)
						{
							// Leer el contenido del archivo
							String[] lines = File.ReadAllLines(file);

							// Obtener el nombre del usuario del contenido del archivo
							String name = lines[1].Split(',')[2].Trim(); // Se asume que el nombre está en la segunda línea


							// Mostrar el nombre del usuario en la consola
							Console.WriteLine(name);
						}
					}
					else
					{
						Console.WriteLine("No se encontraron archivos de usuario.");
					}
				}
				else
				{
					Console.WriteLine("El directorio de usuarios no existe.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al intentar listar los usuarios: {ex.Message}");
			}
		}
	}
}

