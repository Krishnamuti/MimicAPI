using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Database;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        
        public readonly MimicContext _banco;

        public PalavrasController(MimicContext banco)
        {
            _banco = banco;
        }

        //api/palavras
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas(DateTime? data)
        {
            var item = _banco.Palavras.AsQueryable();
            if (data.HasValue)
                item = item.Where(a => a.Criado > data.Value || a.Atualizado > data.Value);

            return Ok(item);
        } 

        //api/palavras/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var palavra = _banco.Palavras.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (palavra == null)
                return NotFound();

            return Ok(palavra);
        }

        //api/palavras
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody]Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
            return Created($"/api/palavras/{palavra.Id}", palavra);
        }

        //api/palavras/1
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody]Palavra palavra)
        {
            var palavraFind = _banco.Palavras.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (palavraFind == null)
                return NotFound();

            palavra.Id = id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return Ok();
        }

        //api/palavras/1
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _banco.Palavras.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (palavra == null)
                return NotFound();

            _banco.Palavras.Remove(_banco.Palavras.Find(id));
            _banco.SaveChanges();
            return Ok();
        }

    }
}
