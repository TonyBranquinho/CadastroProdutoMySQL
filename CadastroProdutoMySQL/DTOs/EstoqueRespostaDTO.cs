namespace CadastroProdutoMySQL.DTOs
{
    public class EstoqueRespostaDTO
    {

        public long Id { get; set; }
        public int Quantidade { get; set; }



        public EstoqueRespostaDTO()
        {
        }


        public EstoqueRespostaDTO(long id, int quantidade)
        {
            Id = id;
            this.Quantidade = quantidade;
        }
    }
}
