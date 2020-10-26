using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.ViewModels
{
    public class UsuarioClaimViewModel
    {
        public Guid UsuarioId { get; set; }
        public IEnumerable<ClaimViewModel> UsuarioClaims { get; set; }
    }

    public class ClaimViewModel
    {
        public EntidadeEnum Entidade { get; set; }
        public string EndPoint { get; set; }
    }
}
