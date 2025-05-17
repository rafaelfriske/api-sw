using api_sw.Models;
using Microsoft.EntityFrameworkCore;

public class MeuContexto : DbContext
{
    // Construtor que aceita DbContextOptions
    public MeuContexto(DbContextOptions<MeuContexto> options) : base(options)
    {

    }

    public DbSet<TbTarefas> TbTarefas { get; set; }
    public DbSet<TbLogin> TbLogin { get; set; }

    public DbSet<TbStatus> TbStatus { get; set; }




}