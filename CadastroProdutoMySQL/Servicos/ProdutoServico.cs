using System;
using CadastroProdutoMySQL.Modelos;

namespace CadastroProdutoMySQL.Servicos
{
    public class ProdutoServico
    {
        // Atributos da classe
        public List<Produto> produtos { get; private set; }
        public List<Estoque> estoque { get; private set; }

        // Metodo vazio
        public ProdutoServico() 
        {
        }

        // Metodo com parametros
        public ProdutoServico(List<Produto> produtos, List<Estoque> estoque)
        {
            this.produtos = produtos;
            this.estoque = estoque;
        }


    }
}
