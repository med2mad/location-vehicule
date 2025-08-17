using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireDigit = false;
		options.User.RequireUniqueEmail = false;
		options.SignIn.RequireConfirmedEmail = false;
		options.SignIn.RequireConfirmedPhoneNumber = false;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

// Role creation
// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     string[] roles = new[] { "Client", "Administrateur", "Super Administrateur" };

//     foreach (var role in roles)
//     {
//         if (!await roleManager.RoleExistsAsync(role))
//         {
//             await roleManager.CreateAsync(new IdentityRole(role));
//         }
//     }
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
var sqlScript = @"
USE [master]
ALTER DATABASE [location_vehicule] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [location_vehicule].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [location_vehicule] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [location_vehicule] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [location_vehicule] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [location_vehicule] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [location_vehicule] SET ARITHABORT OFF 
GO
ALTER DATABASE [location_vehicule] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [location_vehicule] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [location_vehicule] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [location_vehicule] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [location_vehicule] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [location_vehicule] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [location_vehicule] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [location_vehicule] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [location_vehicule] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [location_vehicule] SET  DISABLE_BROKER 
GO
ALTER DATABASE [location_vehicule] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [location_vehicule] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [location_vehicule] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [location_vehicule] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [location_vehicule] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [location_vehicule] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [location_vehicule] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [location_vehicule] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [location_vehicule] SET  MULTI_USER 
GO
ALTER DATABASE [location_vehicule] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [location_vehicule] SET DB_CHAINING OFF 
GO
ALTER DATABASE [location_vehicule] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [location_vehicule] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [location_vehicule] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [location_vehicule] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [location_vehicule] SET QUERY_STORE = ON
GO
ALTER DATABASE [location_vehicule] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [location_vehicule]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conducteurs]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conducteurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](max) NOT NULL,
	[Tel] [nvarchar](max) NOT NULL,
	[CIN] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Conducteurs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Couleurs]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Couleurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Couleurs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Depenses]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Depenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Montant] [decimal](18, 2) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[VehiculeId] [int] NULL,
	[NotificationId] [int] NULL,
 CONSTRAINT [PK_Depenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomClient] [nvarchar](max) NOT NULL,
	[Tel] [nvarchar](max) NOT NULL,
	[LieuDepart] [nvarchar](max) NOT NULL,
	[LieuRetour] [nvarchar](max) NOT NULL,
	[DateDepart] [datetime2](7) NOT NULL,
	[DateRetour] [datetime2](7) NOT NULL,
	[Tarif] [nvarchar](max) NOT NULL,
	[Statut] [nvarchar](max) NOT NULL,
	[VehiculeId] [int] NULL,
	[ConducteurId] [int] NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Models]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Models](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](max) NOT NULL,
	[Marque] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Passagers] [int] NOT NULL,
	[Bagage] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Models] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titre] [nvarchar](max) NOT NULL,
	[Jours] [int] NULL,
	[Mois] [int] NULL,
	[Annees] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paiements]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paiements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Montant] [decimal](18, 2) NOT NULL,
	[Rib] [nvarchar](max) NOT NULL,
	[CVC] [nvarchar](max) NOT NULL,
	[Expiration] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_Paiements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quartiers]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quartiers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](max) NOT NULL,
	[Ville] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Quartiers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Types]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicules]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Immatriculation] [nvarchar](max) NULL,
	[Couleur] [nvarchar](max) NULL,
	[Carburant] [nvarchar](max) NULL,
	[Climatisation] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
	[Prix] [decimal](18, 2) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[ModelId] [int] NULL,
	[KilometrageActuel] [int] NULL,
	[KilometrageEntreVidanges] [int] NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Boite] [nvarchar](max) NULL,
 CONSTRAINT [PK_Vehicules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vidanges]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vidanges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Montant] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[VehiculeId] [int] NULL,
 CONSTRAINT [PK_Vidanges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitesTechniques]    Script Date: 17/08/2025 07:28:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitesTechniques](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Montant] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[VehiculeId] [int] NULL,
 CONSTRAINT [PK_VisitesTechniques] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250325050137_InitialCreate', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250325085551_m1', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250325235356_m2', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327021338_m3', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327215600_m4', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250328093916_m5', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250328101954_m6', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329010403_m7', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329021652_m8', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329081645_9', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329101509_10', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250330060821_11', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250330082635_12', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250331000928_13', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250331104429_14', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250331172444_15', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250331184340_16', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250331193823_17', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250331195107_18', N'8.0.3')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250405183217_19', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250409201251_20', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410000100_21', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410193324_22', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410194652_23', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410195418_24', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410211405_25', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410213003_26', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250411152930_27', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250412172415_28', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417163909_29', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250418231200_30', N'8.0.11')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'78f3103f-d2b1-4d34-9f89-d2317ae2d6e2', N'Client', N'CLIENT', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8c2ad2e8-d894-4f4c-8211-ed315b76c922', N'Super Administrateur', N'SUPER ADMINISTRATEUR', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c3ce2b8a-774b-42e6-a39c-20a6eda103da', N'Administrateur', N'ADMINISTRATEUR', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2f49e941-5697-4d8c-9f72-3a190935e70c', N'8c2ad2e8-d894-4f4c-8211-ed315b76c922')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e74bdec2-d67b-4087-9017-89024fd63f99', N'8c2ad2e8-d894-4f4c-8211-ed315b76c922')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'03c96dc5-0b8d-4638-a941-576f8d2fb2d4', N'c3ce2b8a-774b-42e6-a39c-20a6eda103da')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'353962e4-e855-4f13-83a5-0fcb09913876', N'c3ce2b8a-774b-42e6-a39c-20a6eda103da')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'03c96dc5-0b8d-4638-a941-576f8d2fb2d4', N'nadmin', N'NADMIN', N'nadmin@gmail.com', N'NADMIN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEJcXF2x/FEBIsV2rk4+DD3dunC6FHJjwVhLviPQAAHOF96+uwa221ffC2iUQKrd+/A==', N'VRG2KJP5KQ3RWQCP4ZKVSZ5HEKAQASP5', N'2cc619ba-dd86-4268-ab4a-883b5b60eacd', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2f49e941-5697-4d8c-9f72-3a190935e70c', N'123123', N'123123', N'123123@gmail.com', N'123123@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEFAg8S1Cs8VNMSXO7Ptr/Rsc0ljniETxJ4ZjXc3jrocltr/LoQo2/ECwVkOH58dnjA==', N'YYCHJLIZ3WC22NP63G7XSYRLPXK7NMJ2', N'4150af32-8101-4070-ad71-1f6d473cc83f', N'123123123123', 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'353962e4-e855-4f13-83a5-0fcb09913876', N'111111', N'111111', N'111111@gmail.com', N'111111@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEMSMpnNoBlkeqoMh1+tI6rWhIW9mtw/r51Uc+UfJi6OpTkEvJYLIHt0tR4fyp95PGw==', N'OK6FRI3ZKB7YFWR67WT3TCN7YKVHDWTZ', N'41aafe6e-a3f9-499b-8799-ebdea929c9af', N'111111111', 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e74bdec2-d67b-4087-9017-89024fd63f99', N'11235394', N'11235394', N'11235394@gmail.com', N'11235394@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEIul+rxR3aVy6KPuX+BBa/wlLrK651IL6F1EUCLS1zDBGcitxDdvssI7DGOoXai/wA==', N'UGYKRKH7QAUICT52MN7RWSMGBYFSEA3O', N'ca0efa80-b3f4-4522-b540-3f5109dd8cb4', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Couleurs] ON 
GO
INSERT [dbo].[Couleurs] ([Id], [Nom]) VALUES (1, N'Blanc')
GO
INSERT [dbo].[Couleurs] ([Id], [Nom]) VALUES (2, N'Noir')
GO
INSERT [dbo].[Couleurs] ([Id], [Nom]) VALUES (3, N'Bleu')
GO
INSERT [dbo].[Couleurs] ([Id], [Nom]) VALUES (4, N'Rouge')
GO
INSERT [dbo].[Couleurs] ([Id], [Nom]) VALUES (5, N'Vert')
GO
INSERT [dbo].[Couleurs] ([Id], [Nom]) VALUES (8, N'Gris')
GO
SET IDENTITY_INSERT [dbo].[Couleurs] OFF
GO
SET IDENTITY_INSERT [dbo].[Depenses] ON 
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (1, N'changement de glass
et autres', CAST(123.00 AS Decimal(18, 2)), CAST(N'2025-03-28T00:00:00.0000000' AS DateTime2), 46, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (21, N'Un accident dans l''autoroute', CAST(550.00 AS Decimal(18, 2)), CAST(N'2025-03-01T00:00:00.0000000' AS DateTime2), 46, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (22, NULL, CAST(46.00 AS Decimal(18, 2)), CAST(N'2025-04-01T00:00:00.0000000' AS DateTime2), 45, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (24, NULL, CAST(123.00 AS Decimal(18, 2)), CAST(N'2025-04-12T00:00:00.0000000' AS DateTime2), 46, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (25, NULL, CAST(321.00 AS Decimal(18, 2)), CAST(N'2025-04-12T00:00:00.0000000' AS DateTime2), 46, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (26, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(N'2025-04-12T00:00:00.0000000' AS DateTime2), 46, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (27, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(N'2025-04-12T00:00:00.0000000' AS DateTime2), 46, NULL)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (37, NULL, CAST(500.00 AS Decimal(18, 2)), CAST(N'2025-04-14T00:00:00.0000000' AS DateTime2), 44, 5)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (38, NULL, CAST(100.00 AS Decimal(18, 2)), CAST(N'2025-10-28T00:00:00.0000000' AS DateTime2), 45, 5)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (40, NULL, CAST(321.00 AS Decimal(18, 2)), CAST(N'2024-04-14T00:00:00.0000000' AS DateTime2), 46, 5)
GO
INSERT [dbo].[Depenses] ([Id], [Description], [Montant], [Date], [VehiculeId], [NotificationId]) VALUES (41, NULL, CAST(300.00 AS Decimal(18, 2)), CAST(N'2025-02-13T00:00:00.0000000' AS DateTime2), 46, 5)
GO
SET IDENTITY_INSERT [dbo].[Depenses] OFF
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (18, N'Ahmed Alami', N'0690123456', N'Casablanca', N'Casablanca', CAST(N'2025-03-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-20T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 30, NULL, CAST(N'2025-03-14T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (20, N'Youssef Benani', N'0678901234', N'Casablanca', N'Mekness', CAST(N'2025-02-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-02-25T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 34, NULL, CAST(N'2025-02-23T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (22, N'Omar Cherkaoui', N'0634567890', N'Rabat', N'Casablanca', CAST(N'2025-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-23T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 36, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (23, N'Karim El Mansouri', N'0623456789', N'Casablanca', N'Agadir', CAST(N'2025-03-07T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 37, NULL, CAST(N'2024-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (24, N'Youssef Benani', N'0678901234', N'Rabat', N'Mekness', CAST(N'2025-03-07T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Jour', N'Annulé', 38, NULL, CAST(N'2024-11-07T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (25, N'Mehdi Zouhair', N'0745678901', N'Casablanca', N'Casablanca', CAST(N'2025-02-17T00:00:00.0000000' AS DateTime2), CAST(N'2025-02-27T00:00:00.0000000' AS DateTime2), N'Jour', N'En cours', 40, NULL, CAST(N'2024-10-17T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (26, N'Adil Bennis', N'0790123456', N'Mekness', N'Mekness', CAST(N'2025-04-03T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-13T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 41, NULL, CAST(N'2024-09-22T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (27, N'Youssef Benani', N'0678901234', N'Rabat', N'Casablanca', CAST(N'2025-02-08T00:00:00.0000000' AS DateTime2), CAST(N'2025-02-18T00:00:00.0000000' AS DateTime2), N'Jour', N'Annulé', 42, NULL, CAST(N'2024-08-15T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (28, N'Hamza El Fassi', N'0537123456', N'Agadir', N'Rabat', CAST(N'2025-03-14T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-24T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 43, NULL, CAST(N'2024-07-27T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (29, N'Omar Cherkaoui', N'0634567890', N'Rabat', N'Casablanca', CAST(N'2025-03-11T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-21T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 44, NULL, CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (30, N'Fatima Zahraoui', N'0667890123', N'Casablanca', N'Mekness', CAST(N'2025-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2025-03-23T00:00:00.0000000' AS DateTime2), N'Jour', N'Annulé', 45, NULL, CAST(N'2024-05-06T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (31, N'Bilal Hassani', N'0689012345', N'Casablanca', N'Rabat', CAST(N'2025-01-19T00:00:00.0000000' AS DateTime2), CAST(N'2025-01-29T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 46, NULL, CAST(N'2024-04-05T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (53, N'Adil Bennis', N'0790123456', N'Rabat, Hay Riad', N'Casablanca, Centre ville', CAST(N'2025-04-04T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-14T00:00:00.0000000' AS DateTime2), N'Jour', N'Complet', 34, NULL, CAST(N'2025-04-04T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Locations] ([Id], [NomClient], [Tel], [LieuDepart], [LieuRetour], [DateDepart], [DateRetour], [Tarif], [Statut], [VehiculeId], [ConducteurId], [Date]) VALUES (54, N'Ahmed Alami', N'0690123456', N'Casablanca, Anfa', N'Agadir, Hay Essalam', CAST(N'2025-04-04T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-14T00:00:00.0000000' AS DateTime2), N'Jour', N'En cours', 30, NULL, CAST(N'2025-04-04T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[Models] ON 
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (2, N'Portofino', N'Ferrari', N'Voiture', 4, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (26, N'Corolla', N'Toyota', N'Sports Car', 4, CAST(4.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (28, N'Focus', N'Ford', N'Voiture', 4, CAST(4.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (29, N'Mustang', N'Ford', N'Voiture', 4, CAST(12.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (30, N'Roma', N'Ferrari', N'Voiture', 4, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (31, N'Pajero', N'Mitsubishi', N'Voiture', 4, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (32, N'SF90', N'Ferrari', N'Voiture', 4, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (33, N'Aventador', N'Lamborghini', N'Camionette', 8, CAST(24.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (34, N'Enzo', N'Luxe', N'Voiture', 4, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (35, N'Logan', N'Dacia', N'Voiture', 4, CAST(4.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (36, N'X5', N'BMW', N'Luxe', 5, CAST(10.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (37, N'IX', N'BMW', N'Luxe', 5, CAST(10.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (38, N'C3', N'Citroën', N'Voiture', 5, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (39, N'C4', N'Citroën', N'Voiture', 5, CAST(7.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (40, N'A3', N'Audi', N'Voiture', 4, CAST(7.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (41, N'A4', N'Audi', N'Voiture', 5, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (42, N'Clio', N'Renault', N'Voiture', 5, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (43, N'Mégane', N'Renault', N'Voiture', 5, CAST(9.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (44, N'Classe A', N'Mercedes', N'Voiture', 4, CAST(8.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (45, N'Classe C', N'Mercedes', N'Voiture', 6, CAST(9.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (46, N'Fiesta', N'Ford', N'Voiture', 5, CAST(7.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (47, N'Série 3', N'BMW', N'Voiture', 5, CAST(7.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Models] ([Id], [Nom], [Marque], [Type], [Passagers], [Bagage]) VALUES (48, N'Sandero', N'Dacia', N'Voiture', 4, CAST(4.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Models] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 
GO
INSERT [dbo].[Notifications] ([Id], [Titre], [Jours], [Mois], [Annees], [Description]) VALUES (5, N'Assurances', NULL, NULL, 1, N'L''Assurance a une durée de 1 an, pour protéger financièrement le conducteur.')
GO
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Paiements] ON 
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (4, CAST(3000.00 AS Decimal(18, 2)), N'2525250062385557', N'234', N'04/28', N'alami.ahmed@gmail.com', CAST(N'2025-03-14T00:00:00.0000000' AS DateTime2), 18)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (9, CAST(3550.00 AS Decimal(18, 2)), N'2525250062385557', N'123', N'04/28', N'benani.youssef@gmail.com', CAST(N'2025-02-23T00:00:00.0000000' AS DateTime2), 20)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (10, CAST(3000.00 AS Decimal(18, 2)), N'2525250062385557', N'345', N'04/28', N'cherkaoui222@gmail.com', CAST(N'2025-01-13T00:00:00.0000000' AS DateTime2), 22)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (11, CAST(5500.00 AS Decimal(18, 2)), N'2525250062385557', N'765', N'04/28', N'mansouri.k@gmail.com', CAST(N'2024-12-12T00:00:00.0000000' AS DateTime2), 23)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (12, CAST(6000.00 AS Decimal(18, 2)), N'2525250062385557', N'452', N'04/28', N'benani.youssef@gmail.com', CAST(N'2024-11-07T00:00:00.0000000' AS DateTime2), 24)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (13, CAST(7000.00 AS Decimal(18, 2)), N'2525250062385557', N'745', N'04/28', N'mehdi.zouhair@gmail.com', CAST(N'2024-10-17T00:00:00.0000000' AS DateTime2), 25)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (14, CAST(4000.00 AS Decimal(18, 2)), N'2525250062385557', N'653', N'04/28', N'adil.boss@gmail.com', CAST(N'2024-09-22T00:00:00.0000000' AS DateTime2), 26)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (15, CAST(9000.00 AS Decimal(18, 2)), N'2525250062385557', N'443', N'04/28', N'benani.youssef@gmail.com', CAST(N'2024-08-15T00:00:00.0000000' AS DateTime2), 27)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (16, CAST(5000.00 AS Decimal(18, 2)), N'2525250062385557', N'666', N'04/28', N'hamza.fassi@gmail.com', CAST(N'2024-07-27T00:00:00.0000000' AS DateTime2), 28)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (17, CAST(1000.00 AS Decimal(18, 2)), N'2525250062385557', N'306', N'04/28', N'cherkaoui222@gmail.com', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), 29)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (18, CAST(12000.00 AS Decimal(18, 2)), N'2525250062385557', N'054', N'04/28', N'fatima.zahraoui@gmail.com', CAST(N'2024-05-06T00:00:00.0000000' AS DateTime2), 30)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (19, CAST(5000.00 AS Decimal(18, 2)), N'2525250062385557', N'539', N'04/28', N'bilalni123@gmail.com', CAST(N'2024-04-05T00:00:00.0000000' AS DateTime2), 31)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (23, CAST(5000.00 AS Decimal(18, 2)), N'2525250062385557', N'234', N'04/28', N'bilalni123@gmail.com', CAST(N'2025-03-25T20:50:17.1304534' AS DateTime2), 31)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (38, CAST(6550.00 AS Decimal(18, 2)), N'2525250062385557', N'321', N'22/22', N'adil.bennis@gmail.com', CAST(N'2025-04-04T18:24:06.3613830' AS DateTime2), 53)
GO
INSERT [dbo].[Paiements] ([Id], [Montant], [Rib], [CVC], [Expiration], [Email], [Date], [LocationId]) VALUES (39, CAST(6000.00 AS Decimal(18, 2)), N'2525250062385557', N'432', N'34/34', N'alami.ahmed@gmail.com', CAST(N'2025-06-04T18:57:22.7722531' AS DateTime2), 54)
GO
SET IDENTITY_INSERT [dbo].[Paiements] OFF
GO
SET IDENTITY_INSERT [dbo].[Quartiers] ON 
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (4, N'Agdal', N'Mekness')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (5, N'Centre ville', N'Casablanca')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (6, N'Sbata', N'Casablanca')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (7, N'Sidi el Berbousi', N'Casablanca')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (8, N'Ain sbaa', N'Casablanca')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (10, N'El Mansour', N'Mekness')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (11, N'Hamria', N'Mekness')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (12, N'Hassan', N'Rabat')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (13, N'El Malah', N'Mekness')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (14, N'Souissi', N'Rabat')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (15, N'Taghazout', N'Agadir')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (16, N'Hay Essalam', N'Agadir')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (17, N'Anza', N'Agadir')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (18, N'Kasbah', N'Marrakech')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (19, N'Mellah', N'Marrakech')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (21, N'Médina', N'Marrakech')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (26, N'Hay Riad', N'Rabat')
GO
INSERT [dbo].[Quartiers] ([Id], [Nom], [Ville]) VALUES (27, N'Anfa', N'Casablanca')
GO
SET IDENTITY_INSERT [dbo].[Quartiers] OFF
GO
SET IDENTITY_INSERT [dbo].[Types] ON 
GO
INSERT [dbo].[Types] ([Id], [Nom]) VALUES (1, N'Voiture')
GO
INSERT [dbo].[Types] ([Id], [Nom]) VALUES (2, N'Sports Car')
GO
INSERT [dbo].[Types] ([Id], [Nom]) VALUES (3, N'Luxe')
GO
INSERT [dbo].[Types] ([Id], [Nom]) VALUES (4, N'Camion')
GO
INSERT [dbo].[Types] ([Id], [Nom]) VALUES (5, N'Camionette')
GO
INSERT [dbo].[Types] ([Id], [Nom]) VALUES (8, N'Mini Bus')
GO
SET IDENTITY_INSERT [dbo].[Types] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicules] ON 
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (30, N'CD 7890 E 23', N'Rouge', N'Diesel', N'Sans', N'car_1.jpg', CAST(300.00 AS Decimal(18, 2)), CAST(N'2024-03-27T00:00:00.0000000' AS DateTime2), 2, 44, NULL, N'', N'Manuelle')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (34, N'CD 1224 A 23', N'Blanc', N'Essence', N'Avec', N'car_2.jpg', CAST(355.00 AS Decimal(18, 2)), CAST(N'2025-03-24T00:00:00.0000000' AS DateTime2), 26, 0, NULL, N'', N'Automatique')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (36, N'CD A 12345', N'Blanc', N'Électrique', N'Avec', N'car_3.jpg', CAST(300.00 AS Decimal(18, 2)), CAST(N'2024-12-12T00:00:00.0000000' AS DateTime2), 28, 0, NULL, N'', N'Manuelle')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (37, N'CD 5678 B 23', N'Blanc', N'Hydrogène', N'Sans', N'car_4.jpg', CAST(550.00 AS Decimal(18, 2)), CAST(N'2020-07-15T00:00:00.0000000' AS DateTime2), 29, 0, NULL, N'', N'Automatique')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (38, N'CD B 67890', N'Bleu', N'Hybride', N'Avec', N'car_6.jpg', CAST(600.00 AS Decimal(18, 2)), CAST(N'2025-01-01T00:00:00.0000000' AS DateTime2), 30, 0, NULL, N'', N'Manuelle')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (40, N'CD 9012 C 23', N'Noir', N'Diesel', N'Sans', N'car_7.jpg', CAST(700.00 AS Decimal(18, 2)), CAST(N'2024-05-22T00:00:00.0000000' AS DateTime2), 31, 0, NULL, N'', N'Automatique')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (41, N'CD C 13579', N'Vert', N'Essence', N'Avec', N'car_8.jpg', CAST(800.00 AS Decimal(18, 2)), CAST(N'2024-08-26T00:00:00.0000000' AS DateTime2), 33, 0, NULL, N'', N'Manuelle')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (42, N'CD 3456 D 23', N'Gris', N'Électrique', N'Sans', N'car_9.jpg', CAST(900.00 AS Decimal(18, 2)), CAST(N'2025-03-24T00:00:00.0000000' AS DateTime2), 32, 2000, 700, N'', N'Automatique')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (43, N'CD D 24680', N'Blanc', N'Hydrogène', N'Avec', N'car_5.jpg', CAST(500.00 AS Decimal(18, 2)), CAST(N'2023-08-26T00:00:00.0000000' AS DateTime2), 34, 0, NULL, N'', N'Manuelle')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (44, N'CD E 98765', N'Bleu', N'Hybride', N'Sans', N'car_10.jpg', CAST(100.00 AS Decimal(18, 2)), CAST(N'2024-01-12T00:00:00.0000000' AS DateTime2), 35, 0, NULL, N'', N'Automatique')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (45, N'CD 4676 C 23', N'Rouge', N'Diesel', N'Avec', N'car_12.jpg', CAST(1200.00 AS Decimal(18, 2)), CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 36, 0, NULL, N'', N'Manuelle')
GO
INSERT [dbo].[Vehicules] ([Id], [Immatriculation], [Couleur], [Carburant], [Climatisation], [Photo], [Prix], [Date], [ModelId], [KilometrageActuel], [KilometrageEntreVidanges], [Description], [Boite]) VALUES (46, N'CD 2860 E 53', N'Bleu', N'Essence', N'Sans', N'car_11.jpg', CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-03-30T00:00:00.0000000' AS DateTime2), 37, 100, 1000, N'', N'Automatique')
GO
SET IDENTITY_INSERT [dbo].[Vehicules] OFF
GO
SET IDENTITY_INSERT [dbo].[Vidanges] ON 
GO
INSERT [dbo].[Vidanges] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (17, CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2025-04-13T00:00:00.0000000' AS DateTime2), 44)
GO
INSERT [dbo].[Vidanges] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (18, CAST(500.00 AS Decimal(18, 2)), NULL, CAST(N'2025-05-15T00:00:00.0000000' AS DateTime2), 46)
GO
INSERT [dbo].[Vidanges] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (20, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(N'2024-06-01T00:00:00.0000000' AS DateTime2), 45)
GO
SET IDENTITY_INSERT [dbo].[Vidanges] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitesTechniques] ON 
GO
INSERT [dbo].[VisitesTechniques] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (5, CAST(500.00 AS Decimal(18, 2)), NULL, CAST(N'2025-03-29T00:00:00.0000000' AS DateTime2), 44)
GO
INSERT [dbo].[VisitesTechniques] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (6, CAST(400.00 AS Decimal(18, 2)), NULL, CAST(N'2025-03-29T00:00:00.0000000' AS DateTime2), 37)
GO
INSERT [dbo].[VisitesTechniques] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (15, CAST(45.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-14T00:00:00.0000000' AS DateTime2), 46)
GO
INSERT [dbo].[VisitesTechniques] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (22, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(N'2025-04-14T00:00:00.0000000' AS DateTime2), 46)
GO
INSERT [dbo].[VisitesTechniques] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (23, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), 46)
GO
INSERT [dbo].[VisitesTechniques] ([Id], [Montant], [Description], [Date], [VehiculeId]) VALUES (24, CAST(499.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-11T00:00:00.0000000' AS DateTime2), 45)
GO
SET IDENTITY_INSERT [dbo].[VisitesTechniques] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 17/08/2025 07:28:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 17/08/2025 07:28:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Depenses_NotificationId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_Depenses_NotificationId] ON [dbo].[Depenses]
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Depenses_VehiculeId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_Depenses_VehiculeId] ON [dbo].[Depenses]
(
	[VehiculeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locations_ConducteurId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Locations_ConducteurId] ON [dbo].[Locations]
(
	[ConducteurId] ASC
)
WHERE ([ConducteurId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locations_VehiculeId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_Locations_VehiculeId] ON [dbo].[Locations]
(
	[VehiculeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Paiements_LocationId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_Paiements_LocationId] ON [dbo].[Paiements]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicules_ModelId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicules_ModelId] ON [dbo].[Vehicules]
(
	[ModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vidanges_VehiculeId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_Vidanges_VehiculeId] ON [dbo].[Vidanges]
(
	[VehiculeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_VisitesTechniques_VehiculeId]    Script Date: 17/08/2025 07:28:08 ******/
CREATE NONCLUSTERED INDEX [IX_VisitesTechniques_VehiculeId] ON [dbo].[VisitesTechniques]
(
	[VehiculeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Locations] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [Date]
GO
ALTER TABLE [dbo].[Quartiers] ADD  DEFAULT (N'') FOR [Ville]
GO
ALTER TABLE [dbo].[Vehicules] ADD  DEFAULT (N'') FOR [Description]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Depenses]  WITH CHECK ADD  CONSTRAINT [FK_Depenses_Notifications_NotificationId] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notifications] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Depenses] CHECK CONSTRAINT [FK_Depenses_Notifications_NotificationId]
GO
ALTER TABLE [dbo].[Depenses]  WITH CHECK ADD  CONSTRAINT [FK_Depenses_Vehicules_VehiculeId] FOREIGN KEY([VehiculeId])
REFERENCES [dbo].[Vehicules] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Depenses] CHECK CONSTRAINT [FK_Depenses_Vehicules_VehiculeId]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Conducteurs_ConducteurId] FOREIGN KEY([ConducteurId])
REFERENCES [dbo].[Conducteurs] ([Id])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_Conducteurs_ConducteurId]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Vehicules_VehiculeId] FOREIGN KEY([VehiculeId])
REFERENCES [dbo].[Vehicules] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_Vehicules_VehiculeId]
GO
ALTER TABLE [dbo].[Paiements]  WITH CHECK ADD  CONSTRAINT [FK_Paiements_Locations_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Paiements] CHECK CONSTRAINT [FK_Paiements_Locations_LocationId]
GO
ALTER TABLE [dbo].[Vehicules]  WITH CHECK ADD  CONSTRAINT [FK_Vehicules_Models_ModelId] FOREIGN KEY([ModelId])
REFERENCES [dbo].[Models] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Vehicules] CHECK CONSTRAINT [FK_Vehicules_Models_ModelId]
GO
ALTER TABLE [dbo].[Vidanges]  WITH CHECK ADD  CONSTRAINT [FK_Vidanges_Vehicules_VehiculeId] FOREIGN KEY([VehiculeId])
REFERENCES [dbo].[Vehicules] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Vidanges] CHECK CONSTRAINT [FK_Vidanges_Vehicules_VehiculeId]
GO
ALTER TABLE [dbo].[VisitesTechniques]  WITH CHECK ADD  CONSTRAINT [FK_VisitesTechniques_Vehicules_VehiculeId] FOREIGN KEY([VehiculeId])
REFERENCES [dbo].[Vehicules] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[VisitesTechniques] CHECK CONSTRAINT [FK_VisitesTechniques_Vehicules_VehiculeId]
GO
USE [master]
GO
ALTER DATABASE [location_vehicule] SET  READ_WRITE 
GO
";
sqlScript = sqlScript.Replace("GO", "");


using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ApplicationDbContext>();
	var configuration = services.GetRequiredService<IConfiguration>();

	var dbName = "location_vehicule";

	var masterConnectionString = new SqlConnectionStringBuilder(connectionString)
	{
		InitialCatalog = "master"
	}.ToString();

	using (var masterConnection = new SqlConnection(masterConnectionString))
	{
		await masterConnection.OpenAsync();

		var dropDbCommand = $@"
        IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = '{dbName}')
        BEGIN
            ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            DROP DATABASE [{dbName}];
        END";

		using (var command = new SqlCommand(dropDbCommand, masterConnection))
		{
			await command.ExecuteNonQueryAsync();
		}

		var createDbCommand = $@"
        CREATE DATABASE [{dbName}]
        CONTAINMENT = NONE
        ON PRIMARY 
        (NAME = N'{dbName}', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\{dbName}.mdf', SIZE = 73728KB, FILEGROWTH = 65536KB)
        LOG ON 
        (NAME = N'{dbName}_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\{dbName}_log.ldf', SIZE = 73728KB, FILEGROWTH = 65536KB)";

		using (var command = new SqlCommand(createDbCommand, masterConnection))
		{
			await command.ExecuteNonQueryAsync();
		}
	}

	context.Database.SetConnectionString(connectionString);

	sqlScript = Regex.Replace(sqlScript, @"^\s*GO\s*$", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);

	await context.Database.ExecuteSqlRawAsync(sqlScript);
}


app.Run();
