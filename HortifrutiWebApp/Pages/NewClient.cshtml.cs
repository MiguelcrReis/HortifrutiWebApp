using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Models.Enums;
using HortifrutiWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace HortifrutiWebApp.Pages
{
    public class NewClientModel : PageModel
    {
        private WebAppDbContext _context;
        // Gerenciamento de users
        private UserManager<AppUser> _userManager;
        // Gerenciamento de Perfis
        private RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public NewClientModel(WebAppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        #region Passwords
        public class Passwords
        {
            [Required(ErrorMessage = "\"{0}\" obrigatório.")]
            [StringLength(16, MinimumLength = 6, ErrorMessage = "A \"{0}\" deve conter pelo menos \"{2}\" e no máximo  \"{1}\" caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Required(ErrorMessage = "\"{0}\" obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmarção de Senha")]
            [Compare("Password", ErrorMessage = "A confirmação de senha deve ser igual a senha.")]
            public string ConfirmationPassword { get; set; }
        }
        #endregion

        [BindProperty]
        public Client Client { get; set; }

        [BindProperty]
        public Passwords UserPasswords { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Instancia um novo objeto Client and Address
            var client = new Client();
            client.Address = new Address();
            var userPasswords = new Passwords();

            client.ClientStatus = ClientStatus.Registered;

            if (!await TryUpdateModelAsync(userPasswords, userPasswords.GetType(), nameof(userPasswords)))
                return Page();

            if (await TryUpdateModelAsync(client, Client.GetType(), nameof(Client)))
            {
                // Verifica se existe o perfil de user "client"
                if (!await _roleManager.RoleExistsAsync("client"))
                    await _roleManager.CreateAsync(new IdentityRole("client"));

                // Verifica se já existe user com o e-mail informado
                var existingUser = await _userManager.FindByEmailAsync(client.Email);
                if (existingUser != null)
                {
                    // Adiciona um erro na propriedade Email para retornar erro 
                    ModelState.AddModelError("Client.Email", "Já existe um cliente cadastrado com este e-mail.");
                    return Page();
                }

                // Instância um novo objeto usuário Identity e adiciona ao perfil "client"
                var user = new AppUser()
                {
                    UserName = client.Email,
                    Name = $"{client.Name} {client.LastName}",
                    Email = client.Email,
                    PhoneNumber = client.Phone
                };

                // Cadastra o user no BD
                var result = await _userManager.CreateAsync(user, userPasswords.Password);

                // Se a criação do user Identity foi bem sucedida
                if (result.Succeeded)
                {
                    // Associa o user ao perfil "client"
                    await _userManager.AddToRoleAsync(user, "client");

                    // Add o novo objeto client ao contexto do BD atual e salva
                    _context.Clients.Add(client);
                    int affected = await _context.SaveChangesAsync();

                    // Se salvou o client no BD
                    if (affected > 0)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                        var urlConfirmationEmail = Url.Page("/ConfirmationEmail", null, new { userId = user.Id, token }, Request.Scheme);
                        StringBuilder message = new StringBuilder();
                        message.Append("<h1>HortiFruti Reis :: Confirmação de E-mail</h1>");
                        message.Append($"<p>Por gentileza, <a href='{HtmlEncoder.Default.Encode(urlConfirmationEmail)}'>clique aqui</a> para fazer a confirmação do seu E-mail</p>");
                        message.Append("<p>Atenciosamente, <br>Equipe HortiFruti Reis</p>");
                        await _emailSender.SendEmailAsync(user.Email, "Confirmação de E-mail", "", message.ToString());

                        return RedirectToPage("/RegistrationDone");
                    }
                    else
                    {
                        // Exclui o user Identity criado
                        await _userManager.DeleteAsync(user);
                        ModelState.AddModelError("Client", "Não foi possível concluir o cadastro. Verifique e tente novamente.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError("Client.Address", "Não foi possível criar um usuário com esse e-mail." +
                        "User outro e-mail ou tente recuperar a senha deste.");
                }
            }
            return Page();
        }
    }
}
