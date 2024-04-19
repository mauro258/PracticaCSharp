using System;
using System.Threading.Tasks;

namespace PracticaUsuarios
{
	public class Program
	{
		static async Task Main(String[] args)
		{

			// Instancia del cliente de la API de usuario
			UserApiClient userApiClient = new UserApiClient();

			// Variable para almacenar la opción seleccionada por el usuario
			Int32 opcion;

			// Bucle principal del programa
			do
			{
				// Muestra el menú de opciones
				MostrarMenu();

				// Obtiene la opción seleccionada por el usuario
				opcion = ObtenerOpcion();

				// Limpia la consola antes de mostrar el resultado de la opción seleccionada
				Console.Clear();

				// Ejecuta la lógica correspondiente según la opción seleccionada
				switch (opcion)
				{
					case 1:
						// Opción para guardar los datos de un usuario aleatorio
						await userApiClient.GetUserAndPrintDetails();
						// Espera  segundos antes de limpiar la consola
						await Task.Delay(5000);
						// Limpia la consola
						Console.Clear();
						break;
					case 2:
						Console.WriteLine("Lista de usuarios por nombre");
						Console.WriteLine("--------------------------------");
						// Opción para consultar usuarios por nombre
						// Crear una instancia de UserFileManager
						UserFileManager userFileManager = new UserFileManager();
						// Llamar al método ListUsersByName para listar los usuarios por nombre
						userFileManager.ListUsersByName();
						await Task.Delay(5000);
						// Limpia la consola
						Console.Clear();
						break;
					case 3:
						// Opción para consultar el top 10 por fecha de nacimiento
						Console.WriteLine("Seleccionaste la opción 3");
						break;
					case 4:
						// Opción para salir del programa
						Console.WriteLine("Saliendo del programa...");
						break;
					default:
						// Opción no válida
						Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
						break;
				}

				// Imprime una línea en blanco para separar las iteraciones del bucle
				Console.WriteLine();

			} while (opcion != 4); // Continúa el bucle mientras la opción seleccionada no sea 4 (Salir)
		}

		static void MostrarMenu()
		{
			Console.WriteLine("==============================================");
			Console.WriteLine("                     Menú:");
			Console.WriteLine("1. Guardar datos usuario aleatorio");
			Console.WriteLine("2. Consultar usuarios por nombre");
			Console.WriteLine("3. Consultar el top 10 por fecha de nacimiento");
			Console.WriteLine("4. Salir");
			Console.WriteLine("==============================================");

		}

		static Int32 ObtenerOpcion()
		{
			Console.Write("Ingrese su opción: ");
			Int32 opcion = Convert.ToInt32(Console.ReadLine());
			return opcion;
		}
	}
}
