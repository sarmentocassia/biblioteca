using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario l)
        {
            UsuarioService usuarioService = new UsuarioService();

            if(l.Id == 0)
            {
                usuarioService.Inserir(l);
            }
            else
            {
                usuarioService.Atualizar(l);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            Autenticacao.CheckLogin(this);
          
            UsuarioService usuarioService = new UsuarioService();
            return View(usuarioService.ListarTodos());
        }


        
        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            UsuarioService ls = new UsuarioService();
            Usuario l = ls.ObterPorId(id);
            return View(l);
        }
    }
}