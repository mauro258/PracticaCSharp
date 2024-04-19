using System;

namespace PracticaUsuarios
{
	public class User
	{
		public String Gender { get; set; }
		public String Title { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String Email { get; set; }
		public String Username { get; set; }
		public String DateOfBirth { get; set; }
		public String Address { get; set; }
	}

	public enum Gender
	{
		Masculino,
		Femenino
	}
}
