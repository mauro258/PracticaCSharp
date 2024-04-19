using System;
using System.Threading.Tasks;

namespace PracticaUsuarios
{
	public class Program
	{
		static async Task Main(String[] args)
		{
			UserFileManager userFileManager = new UserFileManager();


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
						Console.WriteLine("Presione cualquier tecla para continuar");
						Console.ReadKey();
						// Limpia la consola
						Console.Clear();
						break;
					case 2:
						Console.WriteLine("Lista de usuarios por nombre");
						Console.WriteLine("--------------------------------");
						// Opción para consultar usuarios por nombre
						// Crear una instancia de UserFileManager
						// Llamar al método ListUsersByName para listar los usuarios por nombre
						userFileManager.ListUsersByName();
						Console.WriteLine("Presione cualquier tecla para continuar");
						Console.ReadKey();
						Console.Clear();

						break;
					case 3:
						//// Obtén el top 10 de los usuarios más antiguos
						userFileManager.GetTop10OldestUsers();
						Console.WriteLine("Presione cualquier tecla para continuar");
						Console.ReadKey();
						Console.Clear();


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
