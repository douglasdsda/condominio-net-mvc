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
    public class VeiculosController : Controller
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public VeiculosController(IVeiculoRepositorio veiculoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        // GET: Veiculos/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeiculoId,Nome,Marca,Cor,Placa,UsuarioId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);
                veiculo.UsuarioId = usuario.Id;
                await _veiculoRepositorio.Inserir(veiculo);
                return RedirectToAction("MinhasInformacoes", "Usuarios");
            }
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
         
            var veiculo = await _veiculoRepositorio.PegarPeloId(id);
            if (veiculo == null)
            {
                return NotFound();
            }
 
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeiculoId,Nome,Marca,Cor,Placa,UsuarioId")] Veiculo veiculo)
        {
            if (id != veiculo.VeiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _veiculoRepositorio.Atualizar(veiculo);
                return RedirectToAction("MinhasInformacoes", "Usuarios");
            }
             
            return View(veiculo);
        }
 
        // POST: Veiculos/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _veiculoRepositorio.Excluir(id);
            return Json("Veículo excluído com sucesso");
        }

    }
}
