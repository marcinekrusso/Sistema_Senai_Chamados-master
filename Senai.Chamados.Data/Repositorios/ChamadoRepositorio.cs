﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Domain.Contratos;
using Senai.Chamados.Domain.Entidades;

namespace Senai.Chamados.Data.Repositorios
{
    public class ChamadoRepositorio : IChamadoRepositorio
    {
        private readonly SenaiChamadosDbContext _contexto;

        public ChamadoRepositorio()
        {
            _contexto = new SenaiChamadosDbContext();
        }

        public bool Alterar(ChamadoDomain domain)
        {

            _contexto.Entry<ChamadoDomain>(domain).State = System.Data.Entity.EntityState.Modified;
            int linhasAlteradas = _contexto.SaveChanges();

            if (linhasAlteradas > 0)
                return true;
            else
                return false;
        }

        public ChamadoDomain BuscarPorId(Guid id, string[] includes = null)
        {
            var query = _contexto.Chamados.AsQueryable();

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(x => x.Id == id);
        }

        public bool Deletar(ChamadoDomain domain)
        {
            var chamado = _contexto.Chamados.Single(x => x.Id == domain.Id);
            _contexto.Chamados.Remove(chamado);

            int linhasDeletadas = _contexto.SaveChanges();

            if (linhasDeletadas > 0)
                return true;
            else
                return false;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public bool Inserir(ChamadoDomain domain)
        {
            _contexto.Chamados.Add(domain);
            int linhasIncluidas = _contexto.SaveChanges();

            if (linhasIncluidas > 0)
                return true;
            else
                return false;
        }

        public List<ChamadoDomain> Listar(Guid idUsuario, string[] includes = null)
        {
            var query = _contexto.Chamados.AsQueryable();

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(x => x.IdUsuario == idUsuario).ToList();
        }

        public List<ChamadoDomain> Listar(string[] includes = null)
        {
            var query = _contexto.Chamados.AsQueryable();

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
    }
}
