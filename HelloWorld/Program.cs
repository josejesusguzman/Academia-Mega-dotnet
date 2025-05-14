using System;
using System.ComponentModel;
using System.Text;

class Program 
{
    // 1) Diccionario con los usuarios válidos y sus contraseñas
    //    Clave   = nombre de usuario
    //    Valor   = contraseña asociada
    private static Dictionary<string, string> usuarios = new Dictionary<string, string>
    {
        {"admin", "qwerty"},
        {"usuario", "pass"},
        {"test", "test"}
    };

    // 2) Constante que define el número máximo de intentos de inicio de sesión
    private const int MAX_ATTEMPTS = 3;

    static void Main(string[] args)
    {
        // 3) Mensajes iniciales en consola
        Console.WriteLine("Este es el programa oficial de Hola Mundo");
        Console.WriteLine("Tienes que iniciar sesión");

        // 4) Invitación al usuario para que escriba su nombre
        Console.WriteLine("Escribe tu usuario");

        // 5) Llamada al método que gestiona el login. Devuelve el usuario si accede correctamente, o null si falla.
        string? usuarioIngresado = TryLogin();

        // 6) Si TryLogin devolvió un nombre (no null), saludamos; si no, avisamos de que se excedieron los intentos.
        if (usuarioIngresado != null)
        {
            Console.WriteLine($"Hola {usuarioIngresado}");
        } 
        else 
        {
            Console.WriteLine("Has excedido el número máximo de intentos");
        }

        // 7) Esperamos a que el usuario presione Enter antes de cerrar la consola
        Console.WriteLine("Presiona Enter para salir");
        Console.ReadLine();
    }

    /// <summary>
    /// 8) Método que controla el proceso de login:
    ///    - Permite MAX_ATTEMPTS intentos.
    ///    - Pide nombre de usuario y contraseña.
    ///    - Valida contra el diccionario `usuarios`.
    ///    - Retorna el nombre de usuario si es válido, o null si se acaban los intentos.
    /// </summary>
    private static string? TryLogin()
    {
        // 8.1) Inicializamos los intentos restantes
        int intentosRestantes = MAX_ATTEMPTS;

        // 8.2) Bucle que repite mientras queden intentos
        while (intentosRestantes > 0)
        {
            Console.WriteLine($"\nIntentos restantes: {intentosRestantes}");
            Console.Write("Ingresa tu numbre de usuario: ");
            string? userLogged = Console.ReadLine();            // 8.2.1) Leemos usuario

            Console.WriteLine("Escribe tu contraseña");
            string? passIngresada = ReadPassword();            // 8.2.2) Leemos contraseña oculta

            // 8.2.3) Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(userLogged) || string.IsNullOrWhiteSpace(passIngresada))
            {
                Console.WriteLine("\nError: El usuario y contraseña no pueden estar vacios.");
                intentosRestantes--;   // descontamos un intento
                continue;              // volvemos al inicio del bucle
            }

            // 8.2.4) Verificamos que el usuario exista y la contraseña coincida
            if (usuarios.ContainsKey(userLogged) &&
                usuarios[userLogged] == passIngresada)
            {
                Console.WriteLine("\nAcceso concedido!!!!");
                return userLogged;     // login exitoso: devolvemos el nombre
            }
            else 
            {
                // 8.2.5) Datos incorrectos: restamos intento y seguimos
                Console.WriteLine("Contraseña y/o usuario incorrectos");
                Console.WriteLine("\nInténtalo de nuevo");
                intentosRestantes--;
            }
        }

        // 8.3) Si salimos del bucle sin éxito, devolvemos null
        return null;
    }

    /// <summary>
    /// 9) Método que lee la contraseña caracter por caracter:
    ///    - Muestra '*' por cada carácter ingresado.
    ///    - Permite borrar con Backspace.
    ///    - Finaliza al presionar Enter y retorna la cadena.
    /// </summary>
    private static string? ReadPassword()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo keyInfo;

        // 9.1) Bucle que continúa hasta detectar Enter
        do
        {
            // 9.1.1) Leemos tecla sin mostrarla en pantalla (true para interceptar)
            keyInfo = Console.ReadKey(true);

            // 9.1.2) Si no es tecla de control (letra, número, símbolo...), la agregamos
            if (!char.IsControl(keyInfo.KeyChar))
            {
                password.Append(keyInfo.KeyChar);
                Console.Write("*");  // mostramos asterisco
            }
            // 9.1.3) Si es Backspace y hay caracteres, borramos el último
            else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                Console.Write("\b \b");  // movemos cursor, borramos '*' y retrocedemos
            }
        }
        // 9.1.4) Seguimos hasta que el usuario presione Enter
        while (keyInfo.Key != ConsoleKey.Enter);

        Console.WriteLine();  // pasamos a la siguiente línea
        return password.ToString();
    }
}
