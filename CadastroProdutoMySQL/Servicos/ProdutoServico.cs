using System;
using CadastroProdutoMySQL.Modelos;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CadastroProdutoMySQL.Servicos
{
    public class ProdutoServico
    {
        // Atributos da classe
        public List<Produto> produtos { get; private set; }
        public List<Estoque> estoque { get; private set; }

        // Construtor vazio
        public ProdutoServico() 
        {
        }

        // Construtor com parametros
        public ProdutoServico(List<Produto> produtos, List<Estoque> estoque)
        {
            this.produtos = produtos;
            this.estoque = estoque;
        }


        // Metodo Cadastro - CREATE
        public void CadastroProduto(Produto novoProduto)
        {
            produtos.Add(novoProduto);
        }


        // Metodo Busca - READ
        public Produto BuscaProduto(int Codigo)
        {
            Produto produtoEncontrado = produtos.FirstOrDefault(p => p.Id == Codigo);
            return produtoEncontrado;
        }


        // Metodo Atualiza - UPDATE
        public void AtualizaProduto(Produto produtoEncontrado, string novoNome, decimal novoPreco)
        {
            produtoEncontrado.Nome = novoNome;
            produtoEncontrado.Preco = novoPreco;
        }


        // Metodo Excluir - DELETE
        public void ExcluirProduto(Produto excluiProduto)
        {
            produtos.Remove(excluiProduto);
        }
    }
}
