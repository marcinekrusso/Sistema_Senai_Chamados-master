using AutoMapper;
using Senai.Chamados.Data.Repositorios;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels.Chamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{

    [Authorize]
    public class ChamadoController : Controller
    {
        // GET: Chamado
        [HttpGet]
        public ActionResult Index()
        {
            ListaChamadoViewModel vmListaChamados = new ListaChamadoViewModel();

            using (ChamadoRepositorio _repoChamado = new ChamadoRepositorio())
            {
                if (User.IsInRole("Administrador"))
                {
                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repoChamado.Listar());
                }
                else
                {
                    var claims = User.Identity as ClaimsIdentity;
                    var id = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    vmListaChamados.ListaChamados = Mapper.Map<List<ChamadoDomain>, List<ChamadoViewModel>>(_repoChamado.Listar(new Guid(id)));

                }

            }

            return View(vmListaChamados);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ChamadoViewModel vmChamado = new ChamadoViewModel();

            return View(vmChamado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ChamadoViewModel chamado)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dados Invalidos";
                    return View(chamado);
                }
                using (ChamadoRepositorio objRepoChamado = new ChamadoRepositorio())
                {
                    var Identity = User.Identity as ClaimsIdentity;
                    var id = Identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    chamado.IdUsuario = new Guid(id);

                    objRepoChamado.Inserir(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                }
                TempData["Sucesso"] = "Chamado Cadastrado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.erro = ex.Message;
                return View(chamado);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Editar(string id)
        {

            try
            {
                // NÃO PRESISA NO GET
                //if (!ModelState.IsValid)
                //{
                //    ViewBag.Erro = "Dados Invalidos";
                //    return View
                //}
                if (id == null)
                {
                    TempData["Erro"] = "Informe o id do usuário";
                    return RedirectToAction("Index");
                }
                ChamadoViewModel vmChamado = new ChamadoViewModel();

                using (ChamadoRepositorio _repChamado = new ChamadoRepositorio())
                {
                    vmChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(_repChamado.BuscarPorId(new Guid(id)));

                    if (vmChamado == null)
                    {
                        TempData["Erro"] = "Chamado não encontrado";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var identiy = User.Identity as ClaimsIdentity;
                        var idUsuario = identiy.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                        if (User.IsInRole("Administrador") || idUsuario == vmChamado.IdUsuario.ToString())
                            return View(vmChamado);
                        else
                        {
                            TempData["Erro"] = "Esse chamdo não pertence a voce PALHAÇO ";
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }




        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ChamadoViewModel chamado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.erro = "Dados Invalidos";
                    return View(chamado);
                }
                using (ChamadoRepositorio vmChamado = new ChamadoRepositorio())
                {


                    vmChamado.Alterar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(chamado));
                    TempData["Sucesso"] = "Chamado alterado";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Excluir(string id)
        {
            if (id == null)
            {
                TempData["Erro"] = "Informe o id do chamado PALHAÇO";
                return RedirectToAction("Index");
            }

            ChamadoViewModel vmChamado = new ChamadoViewModel();

            using (ChamadoRepositorio ObjRepoChamado = new ChamadoRepositorio())
            {
                vmChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(ObjRepoChamado.BuscarPorId(new Guid(id)));

                if (vmChamado == null)
                {
                    TempData["Erro"] = "Usuário não encontrado";
                    return RedirectToAction("Index");
                }
                var identiy = User.Identity as ClaimsIdentity;
                var idUsuario = identiy.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                if (User.IsInRole("Administrador") || idUsuario == vmChamado.IdUsuario.ToString())
                {
                    return View(vmChamado);
                }
                TempData["Erro"] = "Voce não possui permissão";
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO TERMINAR O POST DESSE METODO
        public ActionResult Excluir(ChamadoViewModel chamado)
        {
            if (chamado.Id == Guid.Empty)
            {
                TempData["Erro"] = "Eai o que eu faço?";
                return RedirectToAction("Index");
            }

            if (!User.IsInRole("Administrador"))
            {
                TempData["Erro"] = "Pode naum PALHAÇO!";
                return RedirectToAction("Index");
            }

                using (ChamadoRepositorio _repoChamado = new ChamadoRepositorio())
            {
                ChamadoViewModel vmChamado = Mapper.Map<ChamadoDomain, ChamadoViewModel>(_repoChamado.BuscarPorId(chamado.Id));

                if (vmChamado == null)
                {
                    TempData["Erro"] = "EEEPPPPAAAA tem nada aki naum";
                    return RedirectToAction("Index");
                }
                else
                {
                    _repoChamado.Deletar(Mapper.Map<ChamadoViewModel, ChamadoDomain>(vmChamado));
                    TempData["Erro"] = "Tacale pau nesse carrinho marco véio";
                    return RedirectToAction("Index");
                }
            }
        }
    }
 }

