﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sec.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /FileUpload/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload()
        {
            int arquivosSalvos = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];

                //Suas validações ......

                //Salva o arquivo
                if (arquivo.ContentLength > 0)
                {
                    var uploadPath = Server.MapPath("~/Content/Uploads");
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }
            }

            ViewData["Message"] = String.Format("{0} arquivo(s) salvo(s) com sucesso.",  arquivosSalvos);
            return View("Upload");
        }
    }
}