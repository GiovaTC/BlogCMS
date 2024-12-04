![image](https://github.com/user-attachments/assets/2c739bbb-a69f-40aa-9ad2-c1719583d239)

![image](https://github.com/user-attachments/assets/bcaf133e-ae37-4031-89ed-beeaee23ab1e)

![image](https://github.com/user-attachments/assets/78e627b8-957a-43e0-9284-784dd3029654)

![image](https://github.com/user-attachments/assets/329b7c10-963d-4b95-b8e5-8cc21db0d7d5)

# Blog Personal con Blazor Server

## Descripción
Este proyecto es un sistema de gestión de contenidos (CMS) básico donde los usuarios pueden crear, editar y ver publicaciones.

## Tecnologías Utilizadas
- Blazor Server
- Entity Framework Core
- SQLite como base de datos

## Requisitos Previos
1. Visual Studio 2022
2. .NET 6 o .NET 8 SDK
3. SQL Server o SQLite

---

## Paso 1: Crear un Proyecto Blazor Server
1. Abre Visual Studio 2022.
2. Selecciona **Crear un nuevo proyecto**.
3. Busca y selecciona **Blazor Server App**.
4. Ponle un nombre al proyecto, por ejemplo, `BlogCMS`.
5. Selecciona la versión de .NET deseada (6 u 8) y haz clic en **Crear**.

---

## Paso 2: Configurar el Modelo de Datos
1. Crea una carpeta `Models`.
2. Agrega una clase `Post.cs` con el siguiente contenido:

```csharp
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
```

---

## Paso 3: Configurar Entity Framework Core
1. Instala los paquetes `Microsoft.EntityFrameworkCore.Sqlite` y `Microsoft.EntityFrameworkCore.Tools`.
2. Crea `BlogDbContext` en la carpeta `Data`:

```csharp
using Microsoft.EntityFrameworkCore;

public class BlogDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }
}
```

3. Modifica `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using BlogCMS.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite("Data Source=blog.db"));

var app = builder.Build();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

---

## Paso 4: Crear la Página de Publicaciones
1. Crea `Posts.razor` en `Pages`:

```razor
@page "/posts"
@inject BlogDbContext DbContext

<h3>Publicaciones</h3>

<ul>
    @foreach (var post in posts)
    {
        <li>
            <h4>@post.Title</h4>
            <p>@post.Content</p>
            <small>@post.CreatedAt</small>
        </li>
    }
</ul>

@code {
    private List<Post> posts = new();

    protected override async Task OnInitializedAsync()
    {
        posts = await DbContext.Posts.ToListAsync();
    }
}
```

---

## Paso 5: Crear la Página para Nuevas Publicaciones
1. Crea `NewPost.razor` en `Pages`:

```razor
@page "/new-post"
@inject BlogDbContext DbContext
@inject NavigationManager Navigation

<h3>Nueva Publicación</h3>

<EditForm Model="post" OnValidSubmit="HandleValidSubmit">
    <InputText id="title" @bind-Value="post.Title" placeholder="Título" />
    <br />
    <InputTextArea id="content" @bind-Value="post.Content" placeholder="Contenido" />
    <br />
    <button type="submit">Crear Publicación</button>
</EditForm>

@code {
    private Post post = new();

    private async Task HandleValidSubmit()
    {
        DbContext.Posts.Add(post);
        await DbContext.SaveChangesAsync();
        Navigation.NavigateTo("/posts");
    }
}
```

---

## Paso 6: Migrar la Base de Datos
1. Abre la consola de NuGet en Visual Studio.
2. Ejecuta los comandos:

```powershell
Add-Migration InitialCreate
Update-Database
```

---

## Paso 7: Ejecutar la Aplicación
1. Presiona **F5** o haz clic en **Ejecutar**.
2. Navega a `/posts` para ver las publicaciones y a `/new-post` para agregar nuevas.

---

## Mejoras Futuras
- Agregar funcionalidad de edición y eliminación de publicaciones.
- Implementar autenticación y autorización.
- Añadir comentarios en las publicaciones.

---

## Créditos
Proyecto desarrollado con Blazor Server y Entity Framework Core.

