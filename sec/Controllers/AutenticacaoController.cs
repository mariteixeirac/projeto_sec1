﻿using sec.Models;
using sec.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace sec.Controllers
{
    public class AutenticacaoController : Controller
    {

        private SecContext db = new SecContext();
        // GET: Autenticacao

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.returnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Logar(Usuario usu)
        {
            var usuario = db.Usuarios.Where(u => u.Nick == usu.Nick && u.Senha == usu.Senha).FirstOrDefault();
            if(usuario != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nick),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Nome),
                    new Claim("Id", usuario.Id.ToString())
                }, "ApplicationCookie");


                Request.GetOwinContext().Authentication.SignIn(identity);

                if (!String.IsNullOrWhiteSpace(usu.UrlRetorno) ||
                    Url.IsLocalUrl(usu.UrlRetorno))
                    return Redirect(usu.UrlRetorno);
                else
                    return RedirectToAction("", "Inicio");

            }
            else
            {
                ViewBag.erroLogin = "Usuário não existe!";
                return View("Login");
            }
           
        }
        public ActionResult Cadastrar()
        {
            ViewBag.preferencias = db.Preferencias.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuario dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.preferencias = db.Preferencias.ToList();
                return View(dto);
            }
           

            Usuario u = new Usuario
            {
                Nome = dto.nome,
                Email = dto.email,
                Nick = dto.nick,
                Senha = dto.senha
            };
            if(dto.Prefs != null)
            {
                foreach(var pref in dto.Prefs)
                {
                    u.Preferencias.Add(db.Preferencias.Find(pref));
                }
            }
            db.Usuarios.Add(u);
            db.SaveChanges();
            var identity = new ClaimsIdentity(new[]
              {
                    new Claim(ClaimTypes.Name, u.Nick),
                    new Claim(ClaimTypes.NameIdentifier, u.Nome),
                    new Claim("Id", u.Id.ToString())
                }, "ApplicationCookie");


            Request.GetOwinContext().Authentication.SignIn(identity);
            return RedirectToAction("Index", "Inicio");
        }

        public ActionResult EscolherPreferencias(int id)
        {
            return View();
        }
    }
}