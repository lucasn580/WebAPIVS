﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using webApi.Models;

namespace webApi.Controllers
{
    public class EnderecoesController : ApiController
    {
        private JogaJuntoDBContext db = new JogaJuntoDBContext();

        // GET: api/Enderecoes
        public IList<EnderecoView> GetEnderecoes()
        {
            IQueryable<Endereco> enderecos = db.Enderecoes;
            IList<EnderecoView> retorno = new List<EnderecoView>();

            foreach (var endereco in enderecos)
            {
                retorno.Add(new EnderecoView(endereco));
            }
            return retorno;
        }

        // GET: api/Enderecoes/5
        [ResponseType(typeof(EnderecoView))]
        public async Task<IHttpActionResult> GetEndereco(int id)
        {
            Endereco endereco = await db.Enderecoes.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(new EnderecoView(endereco));
        }

        // PUT: api/Enderecoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endereco.Id_End)
            {
                return BadRequest();
            }

            db.Entry(endereco).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Enderecoes
        [ResponseType(typeof(Endereco))]
        public async Task<IHttpActionResult> PostEndereco(Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enderecoes.Add(endereco);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = endereco.Id_End }, endereco);
        }

        // DELETE: api/Enderecoes/5
        [ResponseType(typeof(Endereco))]
        public async Task<IHttpActionResult> DeleteEndereco(int id)
        {
            Endereco endereco = await db.Enderecoes.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            db.Enderecoes.Remove(endereco);
            await db.SaveChangesAsync();

            return Ok(endereco);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnderecoExists(int id)
        {
            return db.Enderecoes.Count(e => e.Id_End == id) > 0;
        }
    }
}