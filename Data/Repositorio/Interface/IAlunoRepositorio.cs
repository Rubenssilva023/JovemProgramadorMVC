using JovemProgramadorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMVC.Data.Repositorio.Interface
{
    public interface IAlunoRepositorio
    {
        void InserirAluno(AlunoModel aluno);
        List<AlunoModel> BuscarAluno();
        AlunoModel BuscarId(int id);

        void EditarAluno(AlunoModel aluno);
        void ExcluirAluno(AlunoModel aluno);
    }
}
