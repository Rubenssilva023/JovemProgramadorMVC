using JovemProgramadorMVC.Data.Repositorio.Interface;
using JovemProgramadorMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace JovemProgramadorMVC.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConfiguration _configuration;

        public AlunosController(IAlunoRepositorio alunoRepositorio, IConfiguration configuration)
        {
            _alunoRepositorio = alunoRepositorio;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            var alunos = _alunoRepositorio.BuscarAluno();
            return View(alunos);
        }
        public IActionResult Adicionar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            var aluno = _alunoRepositorio.BuscarId(id);
            return View("Editar", aluno);
        }

        public IActionResult Excluir(AlunoModel aluno)
        {
            try {try
                {
                    _alunoRepositorio.ExcluirAluno(aluno);
                    TempData["MensagemSucesso"] = "Aluno Excluido com Sucesso!";
                    return RedirectToAction("Index");
                }
                catch (System.Exception) { throw; } 
            }
            catch (System.Exception) {
                throw;
            }
        }
        public IActionResult Salvar(AlunoModel aluno)
        {
            try {
            _alunoRepositorio.EditarAluno(aluno);
                TempData["MensagemSucesso"] = "Aluno Excluido com Sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception) {
                throw;
            }

        }
        public IActionResult InserirAluno(AlunoModel alunos)
        {
            try {
                _alunoRepositorio.InserirAluno(alunos);

                TempData["MensagemSucesso"] = "Aluno adicionado com Sucesso!";

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            cep = cep.Replace("-", ""); 

            EnderecoModel enderecoModel = new EnderecoModel();

            using var client = new HttpClient();

            var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

            if (result.IsSuccessStatusCode) {
                enderecoModel = System.Text.Json.JsonSerializer.Deserialize<EnderecoModel>(
                await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });
            }

            return View("Endereco", enderecoModel);
        }

    }
}



