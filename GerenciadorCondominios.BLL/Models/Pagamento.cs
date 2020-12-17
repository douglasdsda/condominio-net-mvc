using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Aluguel Aluguel { get; set; }

        public DateTime? DataPagamento { get; set; } = null;

        public StatusPagamento Status { get; set; }
    }

    public enum StatusPagamento { 
        Pago, Pendente, Atrasado
    }
}
