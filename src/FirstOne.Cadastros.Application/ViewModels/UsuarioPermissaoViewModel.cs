using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Application.ViewModels
{
    public class UsuarioPermissaoViewModel
    {
        public Guid UserId { get; set; }
        public IEnumerable<PermissionsViewModel> Permissions { get; set; }
    }

    public class PermissionsViewModel
    {
        public EntidadeEnum Entidade { get; set; }
        public string[] EndPoints { get; set; }
    }
}
