using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorCondominios.Controllers
{
   [Authorize]
    public class EventosController : Controller
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EventosController(IEventoRepositorio eventoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _eventoRepositorio = eventoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        // GET: Eventos
   
        public async Task<IActionResult> Index()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);
            if (await _usuarioRepositorio.VerificarSeUsuarioEstaEmFuncao(usuario, "Morador")) {
                return View(await _eventoRepositorio.PegarEventosPeloId(usuario.Id));
            }
            return View(await _eventoRepositorio.PegarTodos());
        }

     

        // GET: Eventos/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);
            Evento evento = new Evento
            {
                UsuarioId = usuario.Id
            };
         
            return View(evento);
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Nome,Descricao,Data,UsuarioId")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Inserir(evento);
                TempData["NovoRegistro"] = $"Evento {evento.Nome} inserido com sucesso";
                return RedirectToAction(nameof(Index));
            }
   
            return View(evento);
        }

        // GET: Eventos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
       
            var evento = await _eventoRepositorio.PegarPeloId(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Nome,Descricao,Data,UsuarioId")] Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Atualizar(evento);
                TempData["Atualizacao"] = $"Evento {evento.Nome} atualizado";
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }
 

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            await _eventoRepositorio.Excluir(id);
      
            TempData["Exclusao"] = $"Evento excluído";
            return Json("Evento excluído");
        }
 
    }
}
