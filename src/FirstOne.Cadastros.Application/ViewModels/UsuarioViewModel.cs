using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid PessoaId { get; set; }
        public string Role { get; set; }
        public PessoaViewModel Pessoa { get; set; }
        public IEnumerable<ClaimViewModel> UsuarioClaims { get; set; }
    }

}
