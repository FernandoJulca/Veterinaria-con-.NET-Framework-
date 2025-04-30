# Proyecto Veterinaria - ASP.NET MVC (.NET Framework 4.7.2)

Este proyecto es una clínica veterinaria desarrollada con ASP.NET MVC.

## Problema común

**Error**:  
`System.IO.DirectoryNotFoundException: No se puede encontrar una parte de la ruta de acceso 'bin\roslyn\csc.exe'`

### Solución

1. Abre Visual Studio.
2. Ve a: `Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes`.
3. Ejecuta:
   ```powershell
   Update-Package -reinstall
4. Limpia y recompila la solución


### Dependencia importantes
-Microsoft.AspNet.Mvc (5.2.9)

-Microsoft.CodeDom.Providers.DotNetCompilerPlatform (2.0.1)

-.NET Framework 4.7.2